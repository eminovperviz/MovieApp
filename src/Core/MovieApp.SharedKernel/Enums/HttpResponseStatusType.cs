using System.ComponentModel;

namespace MovieApp.SharedKernel.Enums;

public enum HttpResponseStatusType
{
    Default = 0,

    [Description("Unauthorized")]
    Unauthorized = 401,

    [Description("Validation error occured")]
    ValidationError = 1003,

    [Description("Invalid access token")]
    InvalidAccessToken = 1004,

    [Description("Operation cancelled")]
    OperationCancelled = 1005,


    #region Database Exception [3000-5000]

    [Description("Database exception occured")]
    DbException = 3000,

    [Description("Unique key exception occured")]
    UniqeKeyException = 3001,

    [Description("Duplicate key exception occured")]
    DuplicateKeyException = 3002,

    [Description("Foreign key exception occured")]
    ForeignKeyException = 3003,

    [Description("Reference constraint exception occured")]
    ReferfenceConstraintException = 3004,

    #endregion

}
