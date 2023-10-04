// src/pages/AddLinkPage.tsx
import React, {useEffect, useState} from 'react';
import {Console} from "inspector";
import {Grow} from "@mui/material";
import {BehaviorSubject, distinctUntilChanged, Subject, switchMap} from "rxjs";
import {debounceTime, filter, map, tap} from "rxjs/operators";
import {UrlPreview} from "../../hooks/useBffClient/client";
import {Url} from "url";
import useBffClient from "../../hooks/useBffClient";
import UrlPreviewComponent, {UrlPreviewProps} from "../../components/UrlPreview";

const urlInputChange = new Subject<string>();
const urlChangedObservable = urlInputChange.asObservable();

const AddLinkPage: React.FunctionComponent = () => {
    const [link, setLink] = useState<string>('');
    const [topic, setTopic] = useState<string>('');
    const [tags, setTags] = useState<string>('');
    const [note, setNote] = useState<string>('');

    const [urlPreview, setUrlPreview] = useState<UrlPreviewProps | null>(null);

    const client = useBffClient();

    useEffect(() => {
        const subscription = urlChangedObservable
            .pipe(
                tap(() => {
                    setUrlPreview(null);
                }),
                debounceTime(800),
                distinctUntilChanged(),
                switchMap(async value => await getUrlPreview(value)))
            .subscribe(async preview => {

                if(preview) {
                    setUrlPreview({
                        url: preview.url,
                        // title: preview.title ?? null,
                        title: preview.title ?? null,
                        iconUrl: preview.iconUrl,
                        imageUrl: preview.imageUrl,
                        summary: preview.summary ?? null
                    });
                }
                else {
                    setUrlPreview(null);
                }
                // Call getUrlPreview asynchronously
            });

        return () => subscription.unsubscribe()
    }, [])

    const getUrlPreview = async (url: string) : Promise<UrlPreview | null> => {
        
        if(!url)
        {
            return null;            
        }
        
        try {
            return await client.getUrlPreview({url});

            // Call setUrlPreview to a new object using properties from the above preview instance

        } catch (error) {
            console.error(error);
            return null;
        }
    }
    
    const getValidUri = (url : string) : string | null => {

        if (!url.startsWith('http://') && !url.startsWith('https://')) {
            url = 'https://' + url;
        }

        try {
            // The URL constructor will throw an error for invalid URLs
            new URL(url);
            return url;
        } catch (e) {
            return null;
        }
    }

    const handleLinkSubmit = () => {
        console.log("We are submitting a link to " + link)
    };

    return (
        <div style={{display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', background: "red", flex: 1}}>
            <h2>I'll totally read this later</h2>

            <div style={{margin: '20px 0'}}>
                <label>Link:</label>
                <input
                    type="text"
                    onChange={e => urlInputChange.next(e.target.value)}
                    placeholder="Paste link"
                    style={{padding: '10px', marginRight: '10px', width: '300px'}}
                />
            </div>
            
            {urlPreview && <UrlPreviewComponent {...urlPreview} />}
            
            <div style={{margin: '20px 0', width: '300px'}}>
                <label>Topic:</label>
                <input
                    type="text"
                    value={topic}
                    onChange={e => setTopic(e.target.value)}
                    placeholder="e.g., azure/functions"
                    list="topics"
                    style={{width: '100%', padding: '10px'}}
                />
                <datalist id="topics">
                    {/* Render topic suggestions here */}
                    {/* <option value="azure" /> */}
                    {/* <option value="azure/functions" /> */}
                </datalist>
            </div>

            <div style={{margin: '20px 0', width: '300px'}}>
                <label>Tags (comma separated):</label>
                <input
                    type="text"
                    value={tags}
                    onChange={e => setTags(e.target.value)}
                    placeholder="e.g., cloud, serverless"
                    style={{width: '100%', padding: '10px'}}
                />
            </div>

            <div style={{margin: '20px 0', width: '300px'}}>
                <label>Note:</label>
                <textarea
                    value={note}
                    onChange={e => setNote(e.target.value)}
                    rows={4}
                    style={{width: '100%', padding: '10px'}}
                />
            </div>
            <div>
                <button onClick={handleLinkSubmit}>Submit</button>
            </div>
        </div>
    );
};

export default AddLinkPage;
