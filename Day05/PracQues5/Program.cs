using System;
using System.Collections.Generic;
using System.Linq;

class Post
{
    public int PostId;
    public string User;
    public string Topic;
    public string Hashtag;
    public int Engagement;
    public DateTime Time;
}

class Program
{
    // Fast user lookup
    static Dictionary<string, List<Post>> userPosts =
        new Dictionary<string, List<Post>>();

    // Trending hashtags
    static SortedDictionary<int, List<string>> trendingHashtags =
        new SortedDictionary<int, List<string>>(Comparer<int>.Create((x, y) => y.CompareTo(x)));

    // Influencer ranking
    static SortedList<int, string> influencerRanking =
        new SortedList<int, string>();

    // Timeline
    static List<Post> timeline = new List<Post>();

    // Topic hierarchy
    static SortedDictionary<string, Dictionary<int, List<Post>>> topics =
        new SortedDictionary<string, Dictionary<int, List<Post>>>();

    static void AddPost(Post post)
    {
        timeline.Add(post);

        if (!userPosts.ContainsKey(post.User))
            userPosts[post.User] = new List<Post>();

        userPosts[post.User].Add(post);

        if (!trendingHashtags.ContainsKey(post.Engagement))
            trendingHashtags[post.Engagement] = new List<string>();

        trendingHashtags[post.Engagement].Add(post.Hashtag);

        if (!topics.ContainsKey(post.Topic))
            topics[post.Topic] = new Dictionary<int, List<Post>>();

        if (!topics[post.Topic].ContainsKey(post.Engagement))
            topics[post.Topic][post.Engagement] = new List<Post>();

        topics[post.Topic][post.Engagement].Add(post);
    }

    static void GenerateInfluencerRanking()
    {
        influencerRanking.Clear();

        foreach (var user in userPosts)
        {
            int score = user.Value.Sum(x => x.Engagement);

            while (influencerRanking.ContainsKey(score))
                score++;

            influencerRanking.Add(score, user.Key);
        }
    }

    static void Main()
    {
        AddPost(new Post
        {
            PostId = 1,
            User = "Ravi",
            Topic = "Technology",
            Hashtag = "#AI",
            Engagement = 1200,
            Time = DateTime.Now
        });

        AddPost(new Post
        {
            PostId = 2,
            User = "Ananya",
            Topic = "Sports",
            Hashtag = "#Cricket",
            Engagement = 950,
            Time = DateTime.Now
        });

        AddPost(new Post
        {
            PostId = 3,
            User = "Ravi",
            Topic = "Technology",
            Hashtag = "#DotNet",
            Engagement = 800,
            Time = DateTime.Now
        });

        AddPost(new Post
        {
            PostId = 4,
            User = "Rahul",
            Topic = "Technology",
            Hashtag = "#AI",
            Engagement = 1500,
            Time = DateTime.Now
        });

        GenerateInfluencerRanking();

        Console.WriteLine("Trending Hashtags:");
        foreach (var item in trendingHashtags)
        {
            foreach (var tag in item.Value)
                Console.WriteLine($"{tag} -> {item.Key}");
        }

        Console.WriteLine("\nInfluencer Ranking:");
        foreach (var item in influencerRanking.Reverse())
            Console.WriteLine($"{item.Value} : {item.Key}");

        Console.WriteLine("\nTimeline:");
        foreach (var post in timeline)
            Console.WriteLine($"{post.User} - {post.Hashtag}");

        Console.WriteLine("\nTechnology Posts:");
        foreach (var level in topics["Technology"])
        {
            foreach (var post in level.Value)
                Console.WriteLine($"{post.User} - {post.Hashtag} - {post.Engagement}");
        }
    }
}