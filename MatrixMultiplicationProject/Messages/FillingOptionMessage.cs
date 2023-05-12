using System;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MatrixMultiplicationProject.Messages;

public class FillingOptionMessage : ValueChangedMessage<Action<long[,], long[,]>>
{
    public FillingOptionMessage(Action<long[,], long[,]> value) : base(value)
    {
    }
}