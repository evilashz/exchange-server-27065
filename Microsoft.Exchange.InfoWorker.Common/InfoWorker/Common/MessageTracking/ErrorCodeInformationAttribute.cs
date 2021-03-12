using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002A0 RID: 672
	internal class ErrorCodeInformationAttribute : Attribute
	{
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x000564AF File Offset: 0x000546AF
		// (set) Token: 0x060012B2 RID: 4786 RVA: 0x000564B7 File Offset: 0x000546B7
		public bool IsTransientError { get; set; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x000564C0 File Offset: 0x000546C0
		// (set) Token: 0x060012B4 RID: 4788 RVA: 0x000564C8 File Offset: 0x000546C8
		public bool ShowToIWUser { get; set; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x000564D1 File Offset: 0x000546D1
		// (set) Token: 0x060012B6 RID: 4790 RVA: 0x000564D9 File Offset: 0x000546D9
		public bool ShowDetailToIW { get; set; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x000564E2 File Offset: 0x000546E2
		// (set) Token: 0x060012B8 RID: 4792 RVA: 0x000565DC File Offset: 0x000547DC
		public Strings.IDs Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
				if (value <= (Strings.IDs)2837247303U)
				{
					if (value <= Strings.IDs.TrackingErrorLogSearchServiceDown)
					{
						if (value == Strings.IDs.TrackingErrorInvalidADData)
						{
							goto IL_BC;
						}
						if (value != Strings.IDs.TrackingErrorLogSearchServiceDown)
						{
							goto IL_EC;
						}
						this.ErrorFormatter = delegate(TrackingError error, bool isMultiMessageSearch)
						{
							if (string.IsNullOrEmpty(error.Target))
							{
								return this.GetGenericErrorMessageIfMissingArguments(isMultiMessageSearch);
							}
							return string.Format(Strings.GetLocalizedString(this.message), error.Target);
						};
						return;
					}
					else if (value != Strings.IDs.TrackingErrorQueueViewerRpc && value != (Strings.IDs)2319913820U && value != (Strings.IDs)2837247303U)
					{
						goto IL_EC;
					}
				}
				else if (value <= (Strings.IDs)3376565836U)
				{
					if (value != (Strings.IDs)2960157992U && value != (Strings.IDs)3134958540U)
					{
						if (value != (Strings.IDs)3376565836U)
						{
							goto IL_EC;
						}
						goto IL_BC;
					}
				}
				else if (value != (Strings.IDs)3931032456U && value != (Strings.IDs)4118843607U)
				{
					if (value != (Strings.IDs)4287340460U)
					{
						goto IL_EC;
					}
					goto IL_BC;
				}
				this.ErrorFormatter = delegate(TrackingError error, bool isMultiMessageSearch)
				{
					if (string.IsNullOrEmpty(error.Target))
					{
						return this.GetGenericErrorMessageIfMissingArguments(isMultiMessageSearch);
					}
					Strings.IDs key = this.GetMessage(isMultiMessageSearch);
					return string.Format(Strings.GetLocalizedString(key), error.Server, error.Domain, error.Target);
				};
				return;
				IL_BC:
				this.ErrorFormatter = delegate(TrackingError error, bool isMultiMessageSearch)
				{
					if (string.IsNullOrEmpty(error.Data))
					{
						return this.GetGenericErrorMessageIfMissingArguments(isMultiMessageSearch);
					}
					Strings.IDs key = this.GetMessage(isMultiMessageSearch);
					return string.Format(Strings.GetLocalizedString(key), error.Server, error.Domain, error.Data);
				};
				return;
				IL_EC:
				this.ErrorFormatter = ((TrackingError error, bool isMultiMessageSearch) => Strings.GetLocalizedString(this.GetMessage(isMultiMessageSearch)));
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x000566EC File Offset: 0x000548EC
		// (set) Token: 0x060012BA RID: 4794 RVA: 0x000566F4 File Offset: 0x000548F4
		public Strings.IDs MultiMessageSearchMessage { get; set; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x000566FD File Offset: 0x000548FD
		// (set) Token: 0x060012BC RID: 4796 RVA: 0x00056705 File Offset: 0x00054905
		public ErrorCodeInformationAttribute.FormatterMethod ErrorFormatter { get; private set; }

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x0005670E File Offset: 0x0005490E
		// (set) Token: 0x060012BE RID: 4798 RVA: 0x00056716 File Offset: 0x00054916
		public RequiredProperty RequiredProperties { get; set; }

		// Token: 0x060012BF RID: 4799 RVA: 0x0005671F File Offset: 0x0005491F
		private Strings.IDs GetMessage(bool isMultiMessageSearch)
		{
			if (!isMultiMessageSearch || (Strings.IDs)0U >= this.MultiMessageSearchMessage)
			{
				return this.message;
			}
			return this.MultiMessageSearchMessage;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0005673A File Offset: 0x0005493A
		private LocalizedString GetGenericErrorMessageIfMissingArguments(bool isMultiMessageSearch)
		{
			if (isMultiMessageSearch)
			{
				return Strings.GetLocalizedString(this.MultiMessageSearchMessage);
			}
			if (this.IsTransientError)
			{
				return Strings.TrackingTransientError;
			}
			return Strings.TrackingPermanentError;
		}

		// Token: 0x04000CAA RID: 3242
		private static readonly ErrorCodeInformationAttribute.FormatterMethod LogSearchDownFormatter = (TrackingError error, bool isMultiMessageSearch) => string.Format(Strings.TrackingErrorLogSearchServiceDown, error.Target);

		// Token: 0x04000CAB RID: 3243
		private Strings.IDs message;

		// Token: 0x020002A1 RID: 673
		// (Invoke) Token: 0x060012C9 RID: 4809
		internal delegate string FormatterMethod(TrackingError error, bool isMultiMessageSearch);
	}
}
