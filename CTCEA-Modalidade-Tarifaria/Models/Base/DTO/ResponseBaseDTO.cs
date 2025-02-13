namespace CTCEA_Tariff_Modalities.Models.Base.DTO;

public class ResponseBaseDTO<TEntity> where TEntity : class
{
    public bool Error { get; private set; }
    public List<string> Messages { get; private set; }
    public TEntity Result { get; set; }

    public ResponseBaseDTO()
    {
        Messages = new List<string>();
    }

    public void AddError(params string[] messages)
    {
        Error = true;
        Messages.AddRange(messages.ToList());
    }

    public void AddMessageSuccess(params string[] messages)
    {
        Error = false;
        Messages = messages.ToList();
    }
}
