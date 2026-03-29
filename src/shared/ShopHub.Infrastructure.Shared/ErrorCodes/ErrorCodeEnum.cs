using System.ComponentModel;

namespace ShopHub.Infrastructure.Shared.ErrorCodes;

public enum ErrorCodeEnum
{
    #region Common
    [Description(@"Success")]
    COM_SUC_000,
    [Description(@"Unknown")]
    COM_ERR_000,
    [Description(@"Fail validation")]
    COM_ERR_001,
    [Description(@"Duplicate")]
    COM_ERR_002,
    [Description(@"Setting Fail")]
    COM_ERR_003,
    #endregion

    #region User
    [Description(@"User is not found.")]
    USE_ERR_001,
    [Description(@"Username is existing.")]
    USE_ERR_002,
    [Description(@"Email is existing.")]
    USE_ERR_003,
    [Description(@"User is locked.")]
    USE_ERR_004,
    [Description(@"User is deleted.")]
    USE_ERR_005,
    [Description(@"User is wrong password.")]
    USE_ERR_006,
    [Description(@"User is duplicate email.")]
    USE_ERR_007,
    [Description(@"User is duplicate username.")]
    USE_ERR_008,
    [Description(@"User is duplicate phonenumber.")]
    USE_ERR_009,
    [Description(@"Username is null or empty.")]
    USE_ERR_010,
    [Description(@"Username is in use.")]
    USE_ERR_011,
    [Description(@"User is exsiting.")]
    USE_ERR_012,
    #endregion
}