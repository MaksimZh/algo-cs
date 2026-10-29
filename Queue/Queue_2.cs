// Рефлексия
// 6. Динамический массив на основе банковского метода
// Чисто технически, вставка и удаление должны быть дорогими операциями,
// ведь они могут привести к сдвигу больших кусков данных. Тогда модификация
// вначале массива стоит как реаллокация, а в конце - почти ничего.
// Почему это не делается? Видимо, потому, что "банковский счёт" предназначен
// не для точного учёта сложности алгоритма, а для принятия решения о реаллокации.
//
// Больше всего трудностей вызвало удаление элементов. Технической необходимости
// сокращать массив нет, и в нашей реализации копирование всегда происходит при
// изменении размера - даже при сокращении. Вот и получается, что дешевле всего
// вообще не сокращать массив, если только не вводить стоимость "аренды" памяти.
//
// 7. Многомерный динамический массив
// Со вставкой элемента в одномерный массив всё понятно: в конце - добавляем,
// в середине - сдвигаем.
// А вот с многомерным интереснее. Если мы добавляем элемент в конец строки,
// что будет с остальными элементами того же столбца?
// Я в этом случае использовал значение по умолчанию. По своему опыту научных
// расчётов знаю, что пустыми такие столбцы не остаются. В 99.9% случаев цель
// в том, чтобы заполнить весь столбец, так что временное заполнение не очень
// важно.
//
// Для расширения массива я просто увеличивал размер на 1, но сейчас считаю,
// что лучше было использовать запасную ёмкость, как и в одномерном массиве.
// Здесь это ещё важнее, потому что реаллокация многомерного массива требует
// копирования с помощью нетривиального цикла, потому что нельзя заранее
// сказать, сколько вложенных циклов нам потребуется.

namespace AlgorithmsDataStructures;

public static class QueueTools {
  public static void Rotate<T>(Queue<T> queue, int distance) {
    if (queue.Size() == 0) {
      return;
    }
    distance = (distance % queue.Size() + queue.Size()) % queue.Size();
    for (int i = 0; i < distance; ++i) {
      queue.Enqueue(queue.Dequeue());
    }
  }

  public static void Inverse<T>(Queue<T> queue) {
    if (queue.Size() == 0) {
      return;
    }
    Stack<T> stack = new Stack<T>();
    for (; queue.Size() > 0; stack.Push(queue.Dequeue()));
    for (; stack.Count() > 0; queue.Enqueue(stack.Pop()));
  }
}

public class Queue2Stack<T> {
  private Stack<T> stack1_;
  private Stack<T> stack2_;

  public Queue2Stack() {
    stack1_ = new Stack<T>();
    stack2_ = new Stack<T>();
  } 

  public void Enqueue(T item) {
    stack1_.Push(item);
  }

  public T Dequeue() {
    if (stack2_.Count != 0) {
      return stack2_.Pop();
    }
    if (stack1_.Count == 0) {
      return default(T);
    }
    for (; stack1_.Count > 0; stack2_.Push(stack1_.Pop()));
    return stack2_.Pop();
  }

  public int Size() {
    return stack1_.Count + stack2_.Count;
  }
}

public class QueueBuffer<T> {
  private T[] storage_;
  private int count_;
  private int pre_head_;
  private int tail_;

  public QueueBuffer(int capacity) {
    storage_ = new T[capacity];
    pre_head_ = 0;
    tail_ = 0;
  } 

  public void Enqueue(T item) {
    if (count_ == storage_.Length) {
      return;
    }
    ++count_;
    storage_[pre_head_++] = item;
    if (pre_head_ == storage_.Length) {
      pre_head_ = 0;
    }
  }

  public T Dequeue() {
    if (count_ == 0) {
      return default(T);
    }
    --count_;
    T item = storage_[tail_++];
    if (tail_ == storage_.Length) {
      tail_ = 0;
    }
    return item;
  }

  public int Size() {
    return count_;
  }
}
