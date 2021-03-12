using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.ProvisioningMonitoring;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x02000006 RID: 6
	internal sealed class Error
	{
		// Token: 0x06000033 RID: 51 RVA: 0x0000335B File Offset: 0x0000155B
		public Error(Exception exception) : this(exception, null)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003365 File Offset: 0x00001565
		public Error(Exception exception, string errorMessage) : this(exception, null, null)
		{
			this.errorMessage = errorMessage;
			this.errorRecord = new ErrorRecord(exception, string.Empty, ErrorCategory.NotSpecified, null);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000338A File Offset: 0x0000158A
		public Error(Exception exception, string errorMessage, string cmdletName)
		{
			this.errorMessage = errorMessage;
			this.errorRecord = new ErrorRecord(exception, string.Empty, ErrorCategory.NotSpecified, null);
			this.cmdletName = cmdletName;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000033B3 File Offset: 0x000015B3
		public Error(ErrorRecord errorRecord, string cmdletName)
		{
			this.errorRecord = errorRecord;
			this.cmdletName = cmdletName;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000033CC File Offset: 0x000015CC
		public bool IsUserInputError
		{
			get
			{
				return (string.IsNullOrEmpty(this.cmdletName) ? (this.errorRecord.Exception is RecipientTaskException) : ProvisioningMonitoringConfig.IsExceptionWhiteListedForCmdlet(this.errorRecord, this.cmdletName)) || this.IsLegacyUserInputError();
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003418 File Offset: 0x00001618
		public Exception Exception
		{
			get
			{
				return this.errorRecord.Exception;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00003425 File Offset: 0x00001625
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00003441 File Offset: 0x00001641
		public string Message
		{
			get
			{
				return this.errorMessage ?? this.errorRecord.Exception.Message;
			}
			set
			{
				this.errorMessage = value;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000344A File Offset: 0x0000164A
		public override string ToString()
		{
			return this.errorRecord.Exception.ToString();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000345C File Offset: 0x0000165C
		private bool IsLegacyUserInputError()
		{
			bool flag = false;
			if (this.errorRecord != null)
			{
				flag = (this.errorRecord.Exception is InvalidOperationException || this.errorRecord.Exception is ProcessingException || this.errorRecord.Exception is ManagementObjectNotFoundException || this.errorRecord.Exception is DataValidationException || this.errorRecord.Exception is ParameterBindingException || (this.errorRecord.Exception.InnerException != null && this.errorRecord.Exception.InnerException.InnerException != null && this.errorRecord.Exception.InnerException.InnerException is PSInvalidCastException));
				if (!flag && !string.IsNullOrEmpty(this.cmdletName))
				{
					if (string.Equals("New-DistributionGroup", this.cmdletName, StringComparison.OrdinalIgnoreCase) || string.Equals("New-MailContact", this.cmdletName, StringComparison.OrdinalIgnoreCase) || string.Equals("New-MailUser", this.cmdletName, StringComparison.OrdinalIgnoreCase))
					{
						flag = (this.errorRecord.Exception is ManagementObjectAmbiguousException || this.errorRecord.Exception is ADObjectAlreadyExistsException || this.errorRecord.Exception is RecipientTaskException);
					}
					else if (string.Equals("Set-Contact", this.cmdletName, StringComparison.OrdinalIgnoreCase) || string.Equals("set-user", this.cmdletName, StringComparison.OrdinalIgnoreCase) || string.Equals("Set-MailContact", this.cmdletName, StringComparison.OrdinalIgnoreCase) || string.Equals("Set-MailUser", this.cmdletName, StringComparison.OrdinalIgnoreCase) || string.Equals("Set-DistributionGroup", this.cmdletName, StringComparison.OrdinalIgnoreCase))
					{
						flag = (this.errorRecord.Exception is ManagementObjectAmbiguousException || this.errorRecord.Exception is ADObjectAlreadyExistsException || this.errorRecord.Exception is UserWithMatchingWindowsLiveIdExistsException || this.errorRecord.Exception is RecipientTaskException);
					}
					else if (string.Equals("Update-DistributionGroupMember", this.cmdletName, StringComparison.OrdinalIgnoreCase))
					{
						flag = (this.errorRecord.Exception is ManagementObjectAmbiguousException || this.errorRecord.Exception is RecipientTaskException);
					}
				}
			}
			return flag;
		}

		// Token: 0x04000014 RID: 20
		private string errorMessage;

		// Token: 0x04000015 RID: 21
		private string cmdletName;

		// Token: 0x04000016 RID: 22
		private ErrorRecord errorRecord;
	}
}
