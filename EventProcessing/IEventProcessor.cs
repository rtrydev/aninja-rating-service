namespace aninja_rating_service.EventProcessing;

public interface IEventProcessor
{
    Task ProcessEvent(string message);
}