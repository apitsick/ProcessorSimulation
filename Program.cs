using System;
using System.Collections.Generic;

public class BoundedQueue<T>
{
    private T[] queue;
    private int front;
    private int rear;
    private int count;
    private int maxSize;

    public BoundedQueue(int maxSize)
    {
        this.maxSize = maxSize;
        queue = new T[maxSize];
        front = 0;
        rear = -1;
        count = 0;
    }

    public void Enqueue(T item)
    {
        if (count == maxSize)
        {
            throw new InvalidOperationException("Queue is full");
        }
        rear = (rear + 1) % maxSize;
        queue[rear] = item;
        count++;
    }

    public T Dequeue()
    {
        if (count == 0)
        {
            throw new InvalidOperationException("Dequeue from an empty queue");
        }
        T item = queue[front];
        front = (front + 1) % maxSize;
        count--;
        return item;
    }

    public T Peek()
    {
        if (count == 0)
        {
            throw new InvalidOperationException("Peek from an empty queue");
        }
        return queue[front];
    }

    public bool IsEmpty()
    {
        return count == 0;
    }

    public int Size()
    {
        return count;
    }
}

public class BoundedStack<T>
{
    private T[] stack;
    private int top;
    private int maxSize;

    public BoundedStack(int maxSize)
    {
        this.maxSize = maxSize;
        stack = new T[maxSize];
        top = -1;
    }

    public void Push(T item)
    {
        if (top >= maxSize - 1)
        {
            throw new InvalidOperationException("Stack is full");
        }
        stack[++top] = item;
    }

    public T Pop()
    {
        if (top < 0)
        {
            throw new InvalidOperationException("Pop from an empty stack");
        }
        return stack[top--];
    }

    public T Peek()
    {
        if (top < 0)
        {
            throw new InvalidOperationException("Peek from an empty stack");
        }
        return stack[top];
    }

    public bool IsEmpty()
    {
        return top < 0;
    }

    public int Size()
    {
        return top + 1;
    }
}

public class Processor
{
    private BoundedQueue<string> taskQueue;
    private BoundedStack<string> callStack;

    public Processor(int queueSize, int stackSize)
    {
        taskQueue = new BoundedQueue<string>(queueSize);
        callStack = new BoundedStack<string>(stackSize);
    }

    public void AddTask(string task)
    {
        taskQueue.Enqueue(task);
        Console.WriteLine($"Task added: {task}");
    }

    public void ProcessTasks()
    {
        while (!taskQueue.IsEmpty())
        {
            string task = taskQueue.Dequeue();
            Console.WriteLine($"Processing task: {task}");
            ExecuteTask(task);
        }
    }

    private void ExecuteTask(string task)
    {
        callStack.Push(task);
        Console.WriteLine($"Executing task: {task} (Call stack size: {callStack.Size()})");

        // Моделювання виконання завдання
        // Наприклад, виклик вкладеної функції
        if (task.Contains("complex"))
        {
            ExecuteTask("nested function call");
        }

        callStack.Pop();
        Console.WriteLine($"Finished task: {task} (Call stack size: {callStack.Size()})");
    }
}

public class Program
{
    public static void Main()
    {
        Processor processor = new Processor(queueSize: 5, stackSize: 10);

        processor.AddTask("simple task 1");
        processor.AddTask("complex task 2");
        processor.AddTask("simple task 3");

        processor.ProcessTasks();
    }
}