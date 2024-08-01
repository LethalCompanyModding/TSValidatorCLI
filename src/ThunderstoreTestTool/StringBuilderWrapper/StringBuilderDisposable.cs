using System.Text;
using System;

namespace TSTestTool.StringBuilderWrapper;

internal class StringBuilderDisposable : IDisposable
{
    protected StringBuilder MyStringBuilder;
    public int Length => MyStringBuilder.Length;

    public StringBuilderDisposable()
    {
        MyStringBuilder = new();
    }

    public StringBuilderDisposable(string str)
    {
        MyStringBuilder = new(str);
    }

    public StringBuilderDisposable(object obj)
    {
        MyStringBuilder = new(obj.ToString());
    }

    public void Append(object obj) => MyStringBuilder.Append(obj);
    public void AppendLine() => MyStringBuilder.AppendLine();
    public void AppendLine(string str) => MyStringBuilder.AppendLine(str);
    public void Clear() => MyStringBuilder.Clear();

    public void Dispose()
    {
        MyStringBuilder = null!;
    }

    public override string ToString() => MyStringBuilder.ToString();
}
