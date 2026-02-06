public class ReactiveProperty<T>
{
    private T _value;
    public T Value { get => _value; set
        {
            _value = value;
            OnChanged?.Invoke(value);
        }
    }
    public event Action<T> OnChanged;

}
