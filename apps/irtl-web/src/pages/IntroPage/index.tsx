import React from 'react';

const IntroPage = () => {
    return (
        <div style={{ padding: '20px', maxWidth: '800px', margin: 'auto' }}>
            <h1>Welcome to I'll Read That Later</h1>
            <p>
                Ever come across interesting articles, videos, or any content that you want to check out later but end up forgetting?
                irtl is here to help! Save any link or piece of content for later perusal.
            </p>

            <h2>Features:</h2>
            <ul>
                <li>Store links from various sources like websites, YouTube, and more.</li>
                <li>Assign topics to each link, making it easier to categorize and filter them.</li>
                <li>Directly paste and save text content that you'd like to read or refer to later.</li>
                <li>Search functionality to easily find your stored links or text.</li>
            </ul>

            <h2>Roadmap:</h2>
            <ul>
                <li>Intelligent indexing to enhance the search experience.</li>
                <li>Browser extension for even easier link saving.</li>
                <li>Integration with popular platforms for seamless import/export.</li>
                <li>Collaborative lists to share and collaborate on reading materials with friends or colleagues.</li>
            </ul>
        </div>
    );
};

export default IntroPage;