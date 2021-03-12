using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007A4 RID: 1956
	internal class InternalServerErrorException : ServicePermanentException
	{
		// Token: 0x06003A8C RID: 14988 RVA: 0x000CF17C File Offset: 0x000CD37C
		public InternalServerErrorException(Exception innerException) : base(CoreResources.IDs.ErrorInternalServerError, innerException)
		{
			if (Global.WriteStackTraceOnISE)
			{
				InternalServerErrorException ex = innerException as InternalServerErrorException;
				if (ex != null)
				{
					base.ConstantValues.Add("ExceptionClass", ex.ConstantValues["ExceptionClass"]);
					base.ConstantValues.Add("ExceptionMessage", ex.ConstantValues["ExceptionMessage"]);
					base.ConstantValues.Add("StackTrace", ex.ConstantValues["StackTrace"]);
					return;
				}
				base.ConstantValues.Add("ExceptionClass", innerException.GetType().FullName);
				base.ConstantValues.Add("ExceptionMessage", innerException.Message);
				base.ConstantValues.Add("StackTrace", innerException.StackTrace);
			}
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06003A8D RID: 14989 RVA: 0x000CF256 File Offset: 0x000CD456
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}

		// Token: 0x040020A6 RID: 8358
		private const string ExceptionClassKey = "ExceptionClass";

		// Token: 0x040020A7 RID: 8359
		private const string ExceptionMessageKey = "ExceptionMessage";

		// Token: 0x040020A8 RID: 8360
		private const string StackTraceKey = "StackTrace";
	}
}
