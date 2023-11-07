namespace AskMate.Model;

public class Answer
{
    //   - An answer has at least an `id`, a `message`, a `question_id`, and a `submission_time`
    public int Id { get; set; }
    public string Message { get; set; }
    public int Question_id { get; set; }
    public DateTime Submission_time { get; set; }
}