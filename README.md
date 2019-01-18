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


<h3>Custom langage markdown </h3>

The database response can utilize a custom markdown parser to make the AI respond with special properties.
NOTE: "`" is the accepted escape character for using angle brackets i.e < or > 

<h4>Speed</h4>

tag: ```<s val=1></s>```

description: Changes the speed of writing chars to console. 

val= How much faster (if > 1) or slower (< 1) the write speed should be set to. 

NOTE: If opened but never closed will change the the rest of the text writing speed (ex. ```abc <s val=2> test text``` -"abc" will write at normal speed but the rest will write twice as fast)


<h4>Delay</h4>

tag: ```<d val=1>```

description: Pauses writing for a certain amount of time

val= How long (in seconds) the writing should be paused for


<h4>Censor</h4>

tag: ```<cen>```

description: Censors a word by producing a random ASCII word instead


<h4>Ellipsis</h4>

tag: ```<... val=1>```

description: Creates an ellipsis with a short delay inbetween each ".'

val [Optional, default=1]: How long of a delay (in seconds) there should be before each "."


