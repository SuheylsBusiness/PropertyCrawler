<div data-target="readme-toc.content" class="Box-body px-5 pb-5">
            <article class="markdown-body entry-content container-lg" itemprop="text"><h1 align="center" dir="auto">Introducing: PropertyCrawler<br>
</h1>
<p align="center" dir="auto">Ability to scrape properties from immobilienscout24 without any rate limiting (Makes use of unprotected API endpoints)
</p>

<p align="center" dir="auto">
    <a href="https://suheylsbusiness.com/" rel="nofollow"><b>Developer Website</b></a>
</p>  




















<h2 dir="auto"><a id="user-content-node-application" class="anchor" aria-hidden="true" href="#node-application"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>High level explanation of how this solution works</h2>
<p dir="auto">PropertyCrawler makes use of the mobile API endpoints that have no rate limiting to scrape data at a massive scale.</p>



<blockquote>
<p dir="auto">Mobile applications usually have no rate limiter in their API to improve the customer journey.</p>
</blockquote>



















<h1 dir="auto"><a id="user-content-gui" class="anchor" aria-hidden="true" href="#gui"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>How to use</h1>
<p dir="auto">Everything to control the application is to be found in <a href="https://github.com/SuheylsBusiness/PropertyCrawler/tree/main/bin/Release/net6.0/publish">this folder</a></p><p dir="auto">1) Set the locations to scrape listings from inside 'locationList.txt' that is to be found inside the 'Misc' folder.</p><p dir="auto">2) Start 'PropertyCrawler.exe' and whenever the bot is done, a JSON file is being generated containing the output inside the 'Output' folder.</p>


































</article>
          </div>
