using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.ServiceSupport
{
	// Token: 0x020000F3 RID: 243
	[DataContract]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LoadBalanceFault
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00014C64 File Offset: 0x00012E64
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x00014C6C File Offset: 0x00012E6C
		public LoadBalanceFault.LbErrorType ErrorType { get; private set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00014C75 File Offset: 0x00012E75
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x00014C7D File Offset: 0x00012E7D
		public LocalizedString Message { get; private set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00014C86 File Offset: 0x00012E86
		// (set) Token: 0x0600075F RID: 1887 RVA: 0x00014C93 File Offset: 0x00012E93
		[DataMember]
		public byte[] MessageData
		{
			get
			{
				return CommonUtils.ByteSerialize(this.Message);
			}
			set
			{
				this.Message = CommonUtils.ByteDeserialize(value);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00014CA1 File Offset: 0x00012EA1
		// (set) Token: 0x06000761 RID: 1889 RVA: 0x00014CA9 File Offset: 0x00012EA9
		[DataMember]
		public string StackTrace { get; private set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00014CB2 File Offset: 0x00012EB2
		// (set) Token: 0x06000763 RID: 1891 RVA: 0x00014CBA File Offset: 0x00012EBA
		[DataMember]
		public string ExceptionType { get; private set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x00014CC3 File Offset: 0x00012EC3
		// (set) Token: 0x06000765 RID: 1893 RVA: 0x00014CCB File Offset: 0x00012ECB
		[DataMember(EmitDefaultValue = false)]
		public LoadBalanceFault InnerException { get; private set; }

		// Token: 0x06000766 RID: 1894 RVA: 0x00014CD4 File Offset: 0x00012ED4
		public static void Throw(Exception ex)
		{
			LoadBalanceFault loadBalanceFault = LoadBalanceFault.Create(ex);
			throw new FaultException<LoadBalanceFault>(loadBalanceFault, loadBalanceFault.Message);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00014CF9 File Offset: 0x00012EF9
		public void ReconstructAndThrow()
		{
			throw this.Reconstruct();
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00014D04 File Offset: 0x00012F04
		private static LoadBalanceFault Create(Exception ex)
		{
			if (ex == null)
			{
				return null;
			}
			LoadBalanceFault loadBalanceFault = new LoadBalanceFault();
			LocalizedException ex2 = ex as LocalizedException;
			if (ex2 != null)
			{
				loadBalanceFault.Message = ex2.LocalizedString;
			}
			else
			{
				loadBalanceFault.Message = new LocalizedString(ex.Message);
			}
			loadBalanceFault.ExceptionType = CommonUtils.GetFailureType(ex);
			loadBalanceFault.StackTrace = ex.StackTrace;
			loadBalanceFault.ErrorType = (CommonUtils.IsTransientException(ex) ? LoadBalanceFault.LbErrorType.Transient : LoadBalanceFault.LbErrorType.Permanent);
			loadBalanceFault.InnerException = LoadBalanceFault.Create(ex.InnerException);
			return loadBalanceFault;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00014D84 File Offset: 0x00012F84
		private Exception Reconstruct()
		{
			Exception innerException = (this.InnerException != null) ? this.InnerException.Reconstruct() : null;
			LocalizedException result;
			if (this.ErrorType == LoadBalanceFault.LbErrorType.Transient)
			{
				result = new RemoteMailboxLoadBalanceTransientException(this.Message, innerException);
			}
			else
			{
				result = new RemoteMailboxLoadBalancePermanentException(this.Message, innerException);
			}
			return result;
		}

		// Token: 0x020000F4 RID: 244
		public enum LbErrorType
		{
			// Token: 0x040002D9 RID: 729
			Transient,
			// Token: 0x040002DA RID: 730
			Permanent
		}
	}
}
