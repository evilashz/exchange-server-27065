using System;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Replay.Monitoring;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008A8 RID: 2216
	[Serializable]
	public sealed class TransitionInfo
	{
		// Token: 0x06004E2B RID: 20011 RVA: 0x00144530 File Offset: 0x00142730
		private TransitionInfo()
		{
		}

		// Token: 0x17001765 RID: 5989
		// (get) Token: 0x06004E2C RID: 20012 RVA: 0x00144538 File Offset: 0x00142738
		// (set) Token: 0x06004E2D RID: 20013 RVA: 0x00144540 File Offset: 0x00142740
		public TransitionState CurrentState { get; private set; }

		// Token: 0x17001766 RID: 5990
		// (get) Token: 0x06004E2E RID: 20014 RVA: 0x00144549 File Offset: 0x00142749
		// (set) Token: 0x06004E2F RID: 20015 RVA: 0x00144551 File Offset: 0x00142751
		public DateTime? LastActiveTransition { get; private set; }

		// Token: 0x17001767 RID: 5991
		// (get) Token: 0x06004E30 RID: 20016 RVA: 0x0014455A File Offset: 0x0014275A
		// (set) Token: 0x06004E31 RID: 20017 RVA: 0x00144562 File Offset: 0x00142762
		public DateTime? LastInactiveTransition { get; private set; }

		// Token: 0x17001768 RID: 5992
		// (get) Token: 0x06004E32 RID: 20018 RVA: 0x0014456C File Offset: 0x0014276C
		public DateTime? LastTransitionTime
		{
			get
			{
				if (this.CurrentState == TransitionState.Active)
				{
					return this.LastActiveTransition;
				}
				if (this.CurrentState == TransitionState.Inactive)
				{
					return this.LastInactiveTransition;
				}
				return null;
			}
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x001445A2 File Offset: 0x001427A2
		public override string ToString()
		{
			return this.m_toString;
		}

		// Token: 0x17001769 RID: 5993
		// (get) Token: 0x06004E34 RID: 20020 RVA: 0x001445AA File Offset: 0x001427AA
		internal bool IsSuccess
		{
			get
			{
				return this.CurrentState == TransitionState.Active;
			}
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x001445B8 File Offset: 0x001427B8
		internal static TransitionInfo ConstructFromRemoteSerializable(TransientErrorInfoPersisted errorInfo)
		{
			TransitionInfo transitionInfo = new TransitionInfo();
			if (errorInfo != null)
			{
				transitionInfo.CurrentState = TransitionInfo.ConvertTransitionStateFromSerializable(errorInfo.CurrentErrorState);
				transitionInfo.LastActiveTransition = DateTimeHelper.ParseIntoNullableLocalDateTimeIfPossible(errorInfo.LastSuccessTransitionUtc);
				transitionInfo.LastInactiveTransition = DateTimeHelper.ParseIntoNullableLocalDateTimeIfPossible(errorInfo.LastFailureTransitionUtc);
			}
			transitionInfo.m_toString = transitionInfo.GetToString();
			return transitionInfo;
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x00144610 File Offset: 0x00142810
		private static TransitionState ConvertTransitionStateFromSerializable(ErrorTypePersisted errorType)
		{
			switch (errorType)
			{
			case ErrorTypePersisted.Unknown:
				return TransitionState.Unknown;
			case ErrorTypePersisted.Success:
				return TransitionState.Active;
			case ErrorTypePersisted.Failure:
				return TransitionState.Inactive;
			default:
				DiagCore.RetailAssert(false, "Missing case for enumeration: {0}", new object[]
				{
					errorType
				});
				return TransitionState.Unknown;
			}
		}

		// Token: 0x06004E37 RID: 20023 RVA: 0x00144658 File Offset: 0x00142858
		private static string GetTransitionStateString(TransitionState state)
		{
			string text = LocalizedDescriptionAttribute.FromEnum(typeof(TransitionState), state);
			if (!string.IsNullOrWhiteSpace(text))
			{
				return text;
			}
			return state.ToString();
		}

		// Token: 0x06004E38 RID: 20024 RVA: 0x00144690 File Offset: 0x00142890
		private string GetToString()
		{
			return string.Format("{3}: {0}; {4}: {1}; {5}: {2}", new object[]
			{
				this.CurrentState,
				this.LastActiveTransition,
				this.LastInactiveTransition,
				Strings.TransitionInfoLabelCurrentState,
				Strings.TransitionInfoLabelLastSuccessTransition,
				Strings.TransitionInfoLabelLastFailureTransition
			});
		}

		// Token: 0x04002E8D RID: 11917
		private string m_toString;
	}
}
