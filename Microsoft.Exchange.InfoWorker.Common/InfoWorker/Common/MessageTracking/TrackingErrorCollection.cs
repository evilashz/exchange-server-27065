using System;
using System.Collections.Generic;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002FF RID: 767
	internal sealed class TrackingErrorCollection
	{
		// Token: 0x060016CA RID: 5834 RVA: 0x00069FF7 File Offset: 0x000681F7
		internal TrackingErrorCollection()
		{
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x0006A00B File Offset: 0x0006820B
		internal static TrackingErrorCollection Empty
		{
			get
			{
				return TrackingErrorCollection.empty;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x0006A012 File Offset: 0x00068212
		internal List<TrackingError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x0006A01A File Offset: 0x0006821A
		internal static bool IsNullOrEmpty(TrackingErrorCollection trackingErrorCollection)
		{
			return trackingErrorCollection == null || trackingErrorCollection == TrackingErrorCollection.Empty;
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0006A02C File Offset: 0x0006822C
		internal TrackingError Add(ErrorCode errorCode, string target, string data, string exception)
		{
			TrackingError trackingError = new TrackingError(errorCode, target, data, exception);
			this.errors.Add(trackingError);
			return trackingError;
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x0006A051 File Offset: 0x00068251
		internal void Add(TrackingErrorCollection errors)
		{
			if (!TrackingErrorCollection.IsNullOrEmpty(errors))
			{
				this.errors.AddRange(errors.errors);
			}
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0006A06C File Offset: 0x0006826C
		internal void ResetAllErrors()
		{
			this.Errors.Clear();
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x0006A079 File Offset: 0x00068279
		internal void ReadErrorsFromWSMessage(string[] diagnostics, ArrayOfTrackingPropertiesType[] errors)
		{
			if (this.TryReadErrorsFromWSMessage_Exchange2010(diagnostics))
			{
				return;
			}
			this.ReadErrorsFromWSMessage_Exchange2010SP1(errors);
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0006A08C File Offset: 0x0006828C
		private void ReadErrorsFromWSMessage_Exchange2010SP1(ArrayOfTrackingPropertiesType[] propertyBags)
		{
			if (propertyBags != null)
			{
				foreach (ArrayOfTrackingPropertiesType propertyBag in propertyBags)
				{
					TrackingError trackingError = TrackingError.CreateFromWSMessage(propertyBag);
					if (trackingError != null)
					{
						this.errors.Add(trackingError);
					}
				}
			}
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0006A0C8 File Offset: 0x000682C8
		private bool TryReadErrorsFromWSMessage_Exchange2010(string[] diagnostics)
		{
			if (diagnostics == null || diagnostics.Length < 1)
			{
				return false;
			}
			string text = diagnostics[0];
			int num = text.IndexOf("WebServiceError:", StringComparison.Ordinal);
			int num2 = num + "WebServiceError:".Length;
			if (num == -1)
			{
				return false;
			}
			if (num != -1 && num2 < text.Length)
			{
				char c = text[num2];
				if (c != 'C')
				{
					if (c == 'F')
					{
						this.errors.Add(new TrackingError(ErrorCode.GeneralFatalFailure, string.Empty, string.Empty, string.Empty));
						return true;
					}
					switch (c)
					{
					case 'R':
					case 'T':
						break;
					case 'S':
						return true;
					default:
						return true;
					}
				}
				this.errors.Add(new TrackingError(ErrorCode.GeneralTransientFailure, string.Empty, string.Empty, string.Empty));
			}
			return true;
		}

		// Token: 0x04000E9E RID: 3742
		internal const string ErrorLabel = "WebServiceError:";

		// Token: 0x04000E9F RID: 3743
		internal const char TransientErrorValue = 'T';

		// Token: 0x04000EA0 RID: 3744
		internal const char FatalErrorValue = 'F';

		// Token: 0x04000EA1 RID: 3745
		internal const char ConnectionErrorValue = 'C';

		// Token: 0x04000EA2 RID: 3746
		internal const char ReadStatusErrorValue = 'R';

		// Token: 0x04000EA3 RID: 3747
		private static TrackingErrorCollection empty = new TrackingErrorCollection();

		// Token: 0x04000EA4 RID: 3748
		private List<TrackingError> errors = new List<TrackingError>(5);
	}
}
