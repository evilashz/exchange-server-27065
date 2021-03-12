using System;
using System.Linq;
using System.Management.Automation;
using System.Runtime.Serialization;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.DatacenterStrings;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006B9 RID: 1721
	[DataContract]
	public class ErrorRecord : ErrorInformationBase
	{
		// Token: 0x170027D4 RID: 10196
		// (get) Token: 0x0600493F RID: 18751 RVA: 0x000DFD5C File Offset: 0x000DDF5C
		// (set) Token: 0x06004940 RID: 18752 RVA: 0x000DFD64 File Offset: 0x000DDF64
		public ErrorRecord PSErrorRecord { get; set; }

		// Token: 0x06004941 RID: 18753 RVA: 0x000DFD6D File Offset: 0x000DDF6D
		public ErrorRecord()
		{
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x000DFD78 File Offset: 0x000DDF78
		public ErrorRecord(ErrorRecord errorRecord) : base(errorRecord.Exception)
		{
			this.PSErrorRecord = errorRecord;
			this.TargetObject = ((errorRecord.TargetObject != null) ? errorRecord.TargetObject.ToString() : null);
			ErrorDetails errorDetails = errorRecord.ErrorDetails;
			if (errorDetails != null)
			{
				if (!string.IsNullOrEmpty(errorDetails.Message))
				{
					this.Message = errorDetails.Message;
				}
				if (!string.IsNullOrEmpty(errorDetails.RecommendedAction))
				{
					this.RecommendedAction = errorDetails.RecommendedAction;
				}
			}
		}

		// Token: 0x06004943 RID: 18755 RVA: 0x000DFDF0 File Offset: 0x000DDFF0
		public ErrorRecord(Exception exception) : base(exception)
		{
		}

		// Token: 0x170027D5 RID: 10197
		// (set) Token: 0x06004944 RID: 18756 RVA: 0x000DFDF9 File Offset: 0x000DDFF9
		[DataMember]
		public override string Message
		{
			protected set
			{
				base.Message = (ErrorHandlingUtil.AddSourceToErrorMessages ? ("[Error message from cmdlet] " + value) : value);
			}
		}

		// Token: 0x170027D6 RID: 10198
		// (get) Token: 0x06004945 RID: 18757 RVA: 0x000DFE16 File Offset: 0x000DE016
		// (set) Token: 0x06004946 RID: 18758 RVA: 0x000DFE1E File Offset: 0x000DE01E
		[DataMember(EmitDefaultValue = false)]
		public string RecommendedAction { get; set; }

		// Token: 0x170027D7 RID: 10199
		// (get) Token: 0x06004947 RID: 18759 RVA: 0x000DFE27 File Offset: 0x000DE027
		// (set) Token: 0x06004948 RID: 18760 RVA: 0x000DFE2F File Offset: 0x000DE02F
		[DataMember(EmitDefaultValue = false)]
		public string TargetObject { get; set; }

		// Token: 0x170027D8 RID: 10200
		// (get) Token: 0x06004949 RID: 18761 RVA: 0x000DFE38 File Offset: 0x000DE038
		// (set) Token: 0x0600494A RID: 18762 RVA: 0x000DFE40 File Offset: 0x000DE040
		[DataMember(EmitDefaultValue = false)]
		public ErrorRecordContext Context { get; set; }

		// Token: 0x170027D9 RID: 10201
		// (get) Token: 0x0600494B RID: 18763 RVA: 0x000DFE49 File Offset: 0x000DE049
		// (set) Token: 0x0600494C RID: 18764 RVA: 0x000DFE51 File Offset: 0x000DE051
		[DataMember(EmitDefaultValue = false)]
		public string Property { get; set; }

		// Token: 0x170027DA RID: 10202
		// (get) Token: 0x0600494D RID: 18765 RVA: 0x000DFE5A File Offset: 0x000DE05A
		// (set) Token: 0x0600494E RID: 18766 RVA: 0x000DFE62 File Offset: 0x000DE062
		[DataMember(EmitDefaultValue = false)]
		public string Type { get; set; }

		// Token: 0x170027DB RID: 10203
		// (get) Token: 0x0600494F RID: 18767 RVA: 0x000DFE6B File Offset: 0x000DE06B
		// (set) Token: 0x06004950 RID: 18768 RVA: 0x000DFE73 File Offset: 0x000DE073
		[DataMember(EmitDefaultValue = false)]
		public string HelpUrl { get; set; }

		// Token: 0x170027DC RID: 10204
		// (set) Token: 0x06004951 RID: 18769 RVA: 0x000DFE7C File Offset: 0x000DE07C
		public override Exception Exception
		{
			protected set
			{
				base.Exception = value;
				Exception ex = this.Exception;
				if (this.Exception is ParameterBindingException)
				{
					this.Property = (this.Exception as ParameterBindingException).ParameterName;
					if (this.Exception.InnerException != null && this.Exception.InnerException.InnerException != null)
					{
						Exception innerException = this.Exception.InnerException.InnerException;
						ex = ((innerException.InnerException != null && innerException.GetType().Equals(typeof(PSInvalidCastException))) ? innerException.InnerException : innerException);
					}
				}
				else if (this.Exception is DataValidationException)
				{
					this.Property = (this.Exception as DataValidationException).PropertyName;
				}
				if (ex != this.Exception)
				{
					base.Exception = ex;
				}
				this.Message = ex.Message;
				Type type = ex.GetType();
				if (ErrorRecord.ExceptionTypeExposedToClient.Contains(type) || typeof(TransientException).IsInstanceOfType(ex))
				{
					this.Type = type.ToString();
				}
				LocalizedException ex2 = this.Exception as LocalizedException;
				if (ex2 != null)
				{
					this.HelpUrl = HelpUtil.BuildErrorAssistanceUrl(ex2);
				}
			}
		}

		// Token: 0x04003149 RID: 12617
		private static readonly Type[] ExceptionTypeExposedToClient = new Type[]
		{
			typeof(AutoProvisionFailedException),
			typeof(WLCDUnmanagedMemberExistsException),
			typeof(ShouldContinueException),
			typeof(InvalidOperationInDehydratedContextException),
			typeof(NameNotAvailableException),
			typeof(IDSInternalException),
			typeof(WLCDManagedMemberExistsException),
			typeof(PropertyValueExistsException)
		};
	}
}
