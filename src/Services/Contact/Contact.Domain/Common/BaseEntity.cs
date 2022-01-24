namespace Contact.Domain.Common
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; protected set; }
    }
}
