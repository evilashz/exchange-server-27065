using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200003B RID: 59
	[DataContract]
	[KnownType(typeof(MailboxReplicationServiceFault))]
	internal sealed class MailboxReplicationServiceFault
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00004BA2 File Offset: 0x00002DA2
		// (set) Token: 0x060002BF RID: 703 RVA: 0x00004BAA File Offset: 0x00002DAA
		[DataMember]
		public byte[] MessageData { get; private set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00004BB3 File Offset: 0x00002DB3
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x00004BBB File Offset: 0x00002DBB
		[DataMember(EmitDefaultValue = false)]
		public byte[] DataContextData { get; private set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00004BC4 File Offset: 0x00002DC4
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00004BCC File Offset: 0x00002DCC
		[DataMember]
		public string StackTrace { get; private set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00004BD5 File Offset: 0x00002DD5
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00004BDD File Offset: 0x00002DDD
		[DataMember]
		public int[] WKEClasses { get; private set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00004BE6 File Offset: 0x00002DE6
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00004BEE File Offset: 0x00002DEE
		[DataMember]
		public string ExceptionType { get; private set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00004BF7 File Offset: 0x00002DF7
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x00004BFF File Offset: 0x00002DFF
		[DataMember(EmitDefaultValue = false)]
		public MailboxReplicationServiceFault InnerException { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00004C08 File Offset: 0x00002E08
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00004C10 File Offset: 0x00002E10
		[DataMember]
		public int ErrorCode { get; private set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00004C19 File Offset: 0x00002E19
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00004C21 File Offset: 0x00002E21
		[DataMember]
		public int MapiLowLevelError { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00004C2A File Offset: 0x00002E2A
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00004C32 File Offset: 0x00002E32
		[DataMember]
		public MailboxReplicationServiceFault.MRSErrorType MrsErrorType { get; private set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00004C3B File Offset: 0x00002E3B
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00004C43 File Offset: 0x00002E43
		[DataMember]
		public int FlagsInt
		{
			get
			{
				return (int)this.Flags;
			}
			set
			{
				this.Flags = (MRSErrorFlags)value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00004C4C File Offset: 0x00002E4C
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x00004C54 File Offset: 0x00002E54
		public MRSErrorFlags Flags { get; private set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00004C5D File Offset: 0x00002E5D
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x00004C65 File Offset: 0x00002E65
		[DataMember(IsRequired = false)]
		public string ResourceName { get; private set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00004C6E File Offset: 0x00002E6E
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x00004C76 File Offset: 0x00002E76
		[DataMember(IsRequired = false)]
		public string ResourceType { get; private set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00004C7F File Offset: 0x00002E7F
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x00004C87 File Offset: 0x00002E87
		[DataMember(IsRequired = false)]
		public string WlmResourceKey { get; private set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00004C90 File Offset: 0x00002E90
		// (set) Token: 0x060002DB RID: 731 RVA: 0x00004C98 File Offset: 0x00002E98
		[DataMember(IsRequired = false)]
		public int WlmResourceMetricType { get; private set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00004CA1 File Offset: 0x00002EA1
		// (set) Token: 0x060002DD RID: 733 RVA: 0x00004CA9 File Offset: 0x00002EA9
		[DataMember(IsRequired = false)]
		public int Capacity { get; private set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00004CB2 File Offset: 0x00002EB2
		// (set) Token: 0x060002DF RID: 735 RVA: 0x00004CBA File Offset: 0x00002EBA
		[DataMember(IsRequired = false)]
		public double LoadRatio { get; private set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00004CC3 File Offset: 0x00002EC3
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x00004CCB File Offset: 0x00002ECB
		[DataMember(IsRequired = false)]
		public string LoadState { get; private set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00004CD4 File Offset: 0x00002ED4
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x00004CDC File Offset: 0x00002EDC
		[DataMember(IsRequired = false)]
		public string LoadMetric { get; private set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00004CE5 File Offset: 0x00002EE5
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00004CED File Offset: 0x00002EED
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string MessageDataNonLocalized { get; private set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00004CF8 File Offset: 0x00002EF8
		public LocalizedString Message
		{
			get
			{
				if (this.MessageData != null)
				{
					return CommonUtils.ByteDeserialize(this.MessageData);
				}
				if (this.MessageDataNonLocalized != null)
				{
					return new LocalizedString(this.MessageDataNonLocalized);
				}
				return default(LocalizedString);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00004D38 File Offset: 0x00002F38
		public LocalizedString DataContext
		{
			get
			{
				if (this.DataContextData == null)
				{
					return default(LocalizedString);
				}
				return CommonUtils.ByteDeserialize(this.DataContextData);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00004D62 File Offset: 0x00002F62
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00004D83 File Offset: 0x00002F83
		public ExceptionSide Side
		{
			get
			{
				return (((this.Flags & MRSErrorFlags.Source) == MRSErrorFlags.Source) ? ExceptionSide.Source : ExceptionSide.None) | (((this.Flags & MRSErrorFlags.Target) == MRSErrorFlags.Target) ? ExceptionSide.Target : ExceptionSide.None);
			}
			set
			{
				this.Flags = ((this.Flags & ~(MRSErrorFlags.Source | MRSErrorFlags.Target)) | (((value & ExceptionSide.Source) == ExceptionSide.Source) ? MRSErrorFlags.Source : MRSErrorFlags.None) | (((value & ExceptionSide.Target) == ExceptionSide.Target) ? MRSErrorFlags.Target : MRSErrorFlags.None));
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00004DAC File Offset: 0x00002FAC
		public static void Throw(Exception ex)
		{
			MailboxReplicationServiceFault mailboxReplicationServiceFault = MailboxReplicationServiceFault.Create(ex);
			throw new FaultException<MailboxReplicationServiceFault>(mailboxReplicationServiceFault, mailboxReplicationServiceFault.Message);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00004DEC File Offset: 0x00002FEC
		public void ReconstructAndThrow(string serverName, VersionInformation serverVersion)
		{
			ExecutionContext.Create(new DataContext[]
			{
				this.DataContext.IsEmpty ? null : new WrappedDataContext(this.DataContext),
				OperationSideDataContext.GetContext(new ExceptionSide?(this.Side)),
				string.IsNullOrEmpty(serverName) ? null : new WrappedDataContext(MrsStrings.RemoteServerName(serverName))
			}).Execute(delegate
			{
				throw this.Reconstruct(serverVersion);
			});
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00004E84 File Offset: 0x00003084
		private static MailboxReplicationServiceFault Create(Exception ex)
		{
			if (ex == null)
			{
				return null;
			}
			MailboxReplicationServiceFault mailboxReplicationServiceFault = new MailboxReplicationServiceFault();
			if (ex is LocalizedException)
			{
				mailboxReplicationServiceFault.MessageData = CommonUtils.ByteSerialize(((LocalizedException)ex).LocalizedString);
			}
			else
			{
				mailboxReplicationServiceFault.MessageData = CommonUtils.ByteSerialize(new LocalizedString(ex.Message));
			}
			mailboxReplicationServiceFault.Side = (ExecutionContext.GetExceptionSide(ex) ?? ExceptionSide.None);
			mailboxReplicationServiceFault.ExceptionType = CommonUtils.GetFailureType(ex);
			mailboxReplicationServiceFault.StackTrace = ex.StackTrace;
			mailboxReplicationServiceFault.DataContextData = CommonUtils.ByteSerialize(new LocalizedString(ExecutionContext.GetDataContext(ex)));
			mailboxReplicationServiceFault.ErrorCode = CommonUtils.HrFromException(ex);
			mailboxReplicationServiceFault.MapiLowLevelError = CommonUtils.GetMapiLowLevelError(ex);
			mailboxReplicationServiceFault.MrsErrorType = MailboxReplicationServiceFault.ClassifyException(ex);
			WellKnownException[] array = CommonUtils.ClassifyException(ex);
			mailboxReplicationServiceFault.WKEClasses = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				mailboxReplicationServiceFault.WKEClasses[i] = (int)array[i];
			}
			StaticCapacityExceededReservationException ex2 = ex as StaticCapacityExceededReservationException;
			if (ex2 != null)
			{
				mailboxReplicationServiceFault.ResourceName = ex2.ResourceName;
				mailboxReplicationServiceFault.ResourceType = ex2.ResourceType;
				mailboxReplicationServiceFault.Capacity = ex2.Capacity;
			}
			WlmCapacityExceededReservationException ex3 = ex as WlmCapacityExceededReservationException;
			if (ex3 != null)
			{
				mailboxReplicationServiceFault.ResourceName = ex3.ResourceName;
				mailboxReplicationServiceFault.ResourceType = ex3.ResourceType;
				mailboxReplicationServiceFault.WlmResourceKey = ex3.WlmResourceKey;
				mailboxReplicationServiceFault.WlmResourceMetricType = ex3.WlmResourceMetricType;
				mailboxReplicationServiceFault.Capacity = ex3.Capacity;
			}
			WlmResourceUnhealthyException ex4 = ex as WlmResourceUnhealthyException;
			if (ex4 != null)
			{
				mailboxReplicationServiceFault.ResourceName = ex4.ResourceName;
				mailboxReplicationServiceFault.ResourceType = ex4.ResourceType;
				mailboxReplicationServiceFault.WlmResourceKey = ex4.WlmResourceKey;
				mailboxReplicationServiceFault.WlmResourceMetricType = ex4.WlmResourceMetricType;
				mailboxReplicationServiceFault.LoadRatio = ex4.ReportedLoadRatio;
				mailboxReplicationServiceFault.LoadState = ex4.ReportedLoadState;
				mailboxReplicationServiceFault.LoadMetric = ex4.Metric;
			}
			mailboxReplicationServiceFault.InnerException = MailboxReplicationServiceFault.Create(ex.InnerException);
			return mailboxReplicationServiceFault;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00005067 File Offset: 0x00003267
		private static MailboxReplicationServiceFault.MRSErrorType ClassifyException(Exception ex)
		{
			if (ex is MRSProxyConnectionLimitReachedTransientException)
			{
				return MailboxReplicationServiceFault.MRSErrorType.ProxyThrottlingLimitReached;
			}
			if (CommonUtils.IsTransientException(ex))
			{
				return MailboxReplicationServiceFault.MRSErrorType.Transient;
			}
			return MailboxReplicationServiceFault.MRSErrorType.Permanent;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00005080 File Offset: 0x00003280
		private WellKnownException MatchWellKnownException(params WellKnownException[] wkesToCheck)
		{
			if (this.WKEClasses != null)
			{
				for (int i = 0; i < this.WKEClasses.Length; i++)
				{
					foreach (WellKnownException ex in wkesToCheck)
					{
						if (this.WKEClasses[i] == (int)ex)
						{
							return ex;
						}
					}
				}
			}
			return WellKnownException.None;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000050D4 File Offset: 0x000032D4
		private Exception Reconstruct(VersionInformation serverVersion)
		{
			LocalizedException ex = null;
			Exception innerException = (this.InnerException != null) ? this.InnerException.Reconstruct(serverVersion) : null;
			if (this.MrsErrorType == MailboxReplicationServiceFault.MRSErrorType.ProxyThrottlingLimitReached)
			{
				ex = new MRSProxyConnectionLimitReachedTransientException(this.Message, innerException);
			}
			else
			{
				WellKnownException ex2 = this.MatchWellKnownException(new WellKnownException[]
				{
					WellKnownException.StaticCapacityExceededReservation,
					WellKnownException.WlmCapacityExceededReservation,
					WellKnownException.WlmResourceUnhealthy
				});
				if (ex2 != WellKnownException.None)
				{
					switch (ex2)
					{
					case WellKnownException.StaticCapacityExceededReservation:
						ex = new StaticCapacityExceededReservationException(this.ResourceName, this.ResourceType, this.Capacity);
						break;
					case WellKnownException.WlmCapacityExceededReservation:
						ex = new WlmCapacityExceededReservationException(this.ResourceName, this.ResourceType, this.WlmResourceKey, this.WlmResourceMetricType, this.Capacity);
						break;
					case WellKnownException.WlmResourceUnhealthy:
						ex = new WlmResourceUnhealthyException(this.ResourceName, this.ResourceType, this.WlmResourceKey, this.WlmResourceMetricType, this.LoadRatio, this.LoadState, this.LoadMetric);
						break;
					}
				}
				if (ex == null)
				{
					if (this.MrsErrorType == MailboxReplicationServiceFault.MRSErrorType.Transient)
					{
						ex = new RemoteTransientException(this.Message, innerException);
					}
					else
					{
						ex = new RemotePermanentException(this.Message, innerException);
					}
				}
			}
			IMRSRemoteException ex3 = ex as IMRSRemoteException;
			if (ex3 != null)
			{
				ex3.OriginalFailureType = this.ExceptionType;
				ex3.MapiLowLevelError = this.MapiLowLevelError;
				ex3.RemoteStackTrace = this.StackTrace;
				if (this.WKEClasses != null)
				{
					ex3.WKEClasses = new WellKnownException[this.WKEClasses.Length + 1];
					for (int i = 0; i < this.WKEClasses.Length; i++)
					{
						ex3.WKEClasses[i] = (WellKnownException)this.WKEClasses[i];
					}
					ex3.WKEClasses[this.WKEClasses.Length] = WellKnownException.MRSRemote;
				}
				else
				{
					ex3.WKEClasses = CommonUtils.ClassifyException(ex);
				}
				if (serverVersion != null && !serverVersion[17])
				{
					if (!CommonUtils.ExceptionIs(ex, new WellKnownException[]
					{
						WellKnownException.MRSMailboxIsLocked
					}) && (MailboxReplicationServiceFault.DownlevelMailboxIsLockedFailureTypes.Contains(ex3.OriginalFailureType) || this.Message.StringId == MrsStrings.DestMailboxAlreadyBeingMoved.StringId || this.Message.StringId == MrsStrings.SourceMailboxAlreadyBeingMoved.StringId))
					{
						ex3.WKEClasses = new List<WellKnownException>(ex3.WKEClasses)
						{
							WellKnownException.MRSMailboxIsLocked
						}.ToArray();
					}
					if (!CommonUtils.ExceptionIs(ex, new WellKnownException[]
					{
						WellKnownException.MapiNotFound
					}) && ex3.OriginalFailureType == "MapiExceptionNotFound")
					{
						ex3.WKEClasses = new List<WellKnownException>(ex3.WKEClasses)
						{
							WellKnownException.MapiNotFound
						}.ToArray();
					}
				}
			}
			ex.ErrorCode = this.ErrorCode;
			return ex;
		}

		// Token: 0x04000232 RID: 562
		private static readonly HashSet<string> DownlevelMailboxIsLockedFailureTypes = new HashSet<string>
		{
			"MapiExceptionMailboxInTransit",
			"SourceMailboxAlreadyBeingMovedTransientException",
			"SourceMailboxAlreadyBeingMovedPermanentException",
			"DestMailboxAlreadyBeingMovedTransientException",
			"DestMailboxAlreadyBeingMovedPermanentException"
		};

		// Token: 0x0200003C RID: 60
		public enum MRSErrorType
		{
			// Token: 0x04000247 RID: 583
			Permanent = 1,
			// Token: 0x04000248 RID: 584
			Transient,
			// Token: 0x04000249 RID: 585
			ProxyThrottlingLimitReached,
			// Token: 0x0400024A RID: 586
			ResourceUnhealthy
		}
	}
}
