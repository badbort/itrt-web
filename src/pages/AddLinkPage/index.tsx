// src/pages/AddLinkPage.tsx
import React, { useState } from 'react';
import {Console} from "inspector";

const AddLinkPage: React.FunctionComponent = () => {
    const [link, setLink] = useState<string>('');
    const [topic, setTopic] = useState<string>('');
    const [tags, setTags] = useState<string>('');
    const [note, setNote] = useState<string>('');

    const handleLinkSubmit = () => {
        // Logic to submit the link
        console.log("We are submitting a link to " + link)
    };

    // TODO: Add logic to fetch topic suggestions based on user input

    return (
        <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', height: '100vh' }}>
            <h2>I'll totally read this later</h2>

            <div style={{ margin: '20px 0' }}>
                <input
                    type="text"
                    value={link}
                    onChange={e => setLink(e.target.value)}
                    placeholder="Paste link"
                    style={{ padding: '10px', marginRight: '10px', width: '300px' }}
                />
                <button onClick={handleLinkSubmit}>Submit</button>
            </div>

            <div style={{ margin: '20px 0', width: '300px' }}>
                <label>Topic:</label>
                <input
                    type="text"
                    value={topic}
                    onChange={e => setTopic(e.target.value)}
                    placeholder="e.g., azure/functions"
                    list="topics"
                    style={{ width: '100%', padding: '10px' }}
                />
                <datalist id="topics">
                    {/* Render topic suggestions here */}
                    {/* <option value="azure" /> */}
                    {/* <option value="azure/functions" /> */}
                </datalist>
            </div>

            <div style={{ margin: '20px 0', width: '300px' }}>
                <label>Tags (comma separated):</label>
                <input
                    type="text"
                    value={tags}
                    onChange={e => setTags(e.target.value)}
                    placeholder="e.g., cloud, serverless"
                    style={{ width: '100%', padding: '10px' }}
                />
            </div>

            <div style={{ margin: '20px 0', width: '300px' }}>
                <label>Note:</label>
                <textarea
                    value={note}
                    onChange={e => setNote(e.target.value)}
                    rows={4}
                    style={{ width: '100%', padding: '10px' }}
                />
            </div>
        </div>
    );
};

export default AddLinkPage;
