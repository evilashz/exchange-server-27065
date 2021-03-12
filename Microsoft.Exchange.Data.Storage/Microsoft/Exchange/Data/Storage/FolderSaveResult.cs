using System;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000061 RID: 97
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FolderSaveResult
	{
		// Token: 0x06000718 RID: 1816 RVA: 0x000384FC File Offset: 0x000366FC
		public FolderSaveResult(OperationResult operationResult, LocalizedException exception, PropertyError[] propertyErrors)
		{
			EnumValidator.ThrowIfInvalid<OperationResult>(operationResult, "operationResult");
			this.OperationResult = operationResult;
			this.PropertyErrors = propertyErrors;
			this.Exception = exception;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00038524 File Offset: 0x00036724
		public LocalizedException ToException()
		{
			int errorCount = 0;
			StringBuilder stringBuilder = new StringBuilder();
			if (this.PropertyErrors != null)
			{
				errorCount = this.PropertyErrors.Length;
				foreach (PropertyError propertyError in this.PropertyErrors)
				{
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(propertyError.ToLocalizedString());
					if (stringBuilder.Length >= 20000)
					{
						stringBuilder.Length = 20000 - "...".Length;
						stringBuilder.Append("...");
						break;
					}
				}
			}
			return this.ToException(ServerStrings.FolderSaveOperationResult(this.GetLocalizedResult(), errorCount, stringBuilder.ToString()));
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x000385CF File Offset: 0x000367CF
		public LocalizedException ToException(LocalizedString exceptionMessage)
		{
			if (this.IsErrorTransient)
			{
				return new FolderSaveTransientException(exceptionMessage, this);
			}
			return new FolderSaveException(exceptionMessage, this);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000385E8 File Offset: 0x000367E8
		public override string ToString()
		{
			if (this.OperationResult == OperationResult.Succeeded)
			{
				return this.OperationResult.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder(this.OperationResult.ToString());
			if (this.PropertyErrors != null)
			{
				foreach (PropertyError propertyError in this.PropertyErrors)
				{
					stringBuilder.Append(string.Format(", {0}", propertyError.ToString()));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00038664 File Offset: 0x00036864
		private LocalizedString GetLocalizedResult()
		{
			switch (this.OperationResult)
			{
			case OperationResult.Succeeded:
				return ServerStrings.OperationResultSucceeded;
			case OperationResult.Failed:
				return ServerStrings.OperationResultFailed;
			case OperationResult.PartiallySucceeded:
				return ServerStrings.OperationResultPartiallySucceeded;
			default:
				throw new NotImplementedException(this.OperationResult.ToString());
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x000386B8 File Offset: 0x000368B8
		private bool IsErrorTransient
		{
			get
			{
				if (this.OperationResult != OperationResult.Succeeded)
				{
					foreach (PropertyError propertyError in this.PropertyErrors)
					{
						if (propertyError.PropertyErrorCode != PropertyErrorCode.TransientMapiCallFailed)
						{
							return false;
						}
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x040001EA RID: 490
		private const int MaxErrorMessageLength = 20000;

		// Token: 0x040001EB RID: 491
		public readonly OperationResult OperationResult;

		// Token: 0x040001EC RID: 492
		public readonly PropertyError[] PropertyErrors;

		// Token: 0x040001ED RID: 493
		public readonly LocalizedException Exception;
	}
}
