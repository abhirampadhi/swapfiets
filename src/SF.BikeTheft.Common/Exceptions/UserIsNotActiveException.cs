﻿namespace SF.BikeTheft.Common.Exceptions;
public class UserIsNotActiveException : Exception
{
    public UserIsNotActiveException() : base("User is not active")
    { }
}
