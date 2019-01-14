# P.A.L
Personal Assistance Lexicon

An artificial intelligence that reads user inputs as requests in the form of natural language (english) and returns the best response described in <code>database.json</code>

<hr>

The AI is as smart as the database (more results = more intelligence) but does many things for user input parsing such as:

-Spell Checking (uses SymSpell: https://github.com/wolfgarbe/SymSpell)

-Contextual Parsing (ex. Who is Michael Bitzos? Where is <strong>he</strong>?)

-Sentiment Analysis (if the statement is a positive or negative sentiment) (uses VaderSharp: https://github.com/codingupastorm/vadersharp)

-Synonym replacement (ex. Where is the house? Where is the <strong>building</strong>?) (uses Syn.WordNet: https://developer.syn.co.in/tutorial/wordnet/tutorial.html and WordNet indices: https://wordnet.princeton.edu/)

-Cosine Similarity for database best match (uses F23.StringSimilarity: https://github.com/feature23/StringSimilarity.NET)

<hr>

<h3>Requirements:</h3>

-NuGet Package Manager

-.Net Core SDK 2.0+

<h3>How to run:</h3>

simply run the following command: <code>dotnet run</code> and begin interacting with the AI!

<hr>

<h3>Configuration</h3>

<h4>config.json</h4>

SimilarityThreshold: decimal (<1) default: 0.75
     
     -The minimum similarity score for a match

MinSimilarityThreshold: decimal (<1) default: 0.25
    
    - Minimum similarity score to skip synonyms and return a "not-found" response

MaxSimilarityThreshold: decimal (<1) default : 0.9
   
    - Similarty score to skip synonyms

MaxSynonyms: integer (>0) default: 10
   
    - Max amount of synonyms to use, WARNING: large number will cause program to run for a VERY long time
    
<h4>log.config.json</h4>

ShowSentiment: bool default: true
    
    -If we should log sentiment score

ShowSimilarityScore: bool default: true
   
    -If we should log the similarity score of the input to the returned response

ShowDatabasedRequestUsed: bool default: true
    
    -If we should show what request was used for the response

ShowTimeElasped: bool default: true
    
    -If we should show the elapsed time to retrieve a response
