using System;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009C4 RID: 2500
	internal abstract class LicenseManager : IDisposeTrackable, IDisposable
	{
		// Token: 0x0600367C RID: 13948 RVA: 0x0008ACE3 File Offset: 0x00088EE3
		internal LicenseManager(IWSManagerPerfCounters perfcounters, IRmsLatencyTracker latencyTracker)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.perfcounters = (perfcounters ?? NoopWSManagerPerfCounters.Instance);
			this.rmsLatencyTracker = (latencyTracker ?? NoopRmsLatencyTracker.Instance);
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x0008AD17 File Offset: 0x00088F17
		public virtual void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x0008AD39 File Offset: 0x00088F39
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<LicenseManager>(this);
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x0008AD41 File Offset: 0x00088F41
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x0008AD58 File Offset: 0x00088F58
		internal IAsyncResult BeginAcquireLicense(XmlNode[] issuanceLicense, LicenseIdentity[] identities, AsyncCallback callback, object state)
		{
			if (identities == null)
			{
				throw new ArgumentNullException("identities");
			}
			if (issuanceLicense == null)
			{
				throw new ArgumentNullException("issuanceLicense");
			}
			LicenseManager.LicenseState worker = new LicenseManager.LicenseState(issuanceLicense, identities);
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(worker, state, callback);
			this.IssueNewWebRequest(lazyAsyncResult);
			return lazyAsyncResult;
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x0008AD9C File Offset: 0x00088F9C
		internal LicenseResponse[] EndAcquireLicense(IAsyncResult asyncResult)
		{
			ExTraceGlobals.RightsManagementTracer.TraceDebug((long)this.GetHashCode(), "EndAcquireLicense invoked.");
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult == null)
			{
				throw new InvalidOperationException("asyncResult has to be type of LazyAsyncResult.");
			}
			if (!lazyAsyncResult.IsCompleted)
			{
				lazyAsyncResult.InternalWaitForCompletion();
			}
			RightsManagementException ex = lazyAsyncResult.Result as RightsManagementException;
			if (ex != null)
			{
				throw ex;
			}
			LicenseManager.LicenseState licenseState = lazyAsyncResult.AsyncObject as LicenseManager.LicenseState;
			if (licenseState == null)
			{
				throw new InvalidOperationException("result.AsyncObject cannot be null here.");
			}
			return licenseState.Responses;
		}

		// Token: 0x06003682 RID: 13954
		protected abstract void IssueNewWebRequest(LazyAsyncResult asyncResult);

		// Token: 0x06003683 RID: 13955
		protected abstract void AcquireLicenseCallback(IAsyncResult asyncResult);

		// Token: 0x04002EAF RID: 11951
		private DisposeTracker disposeTracker;

		// Token: 0x04002EB0 RID: 11952
		protected IWSManagerPerfCounters perfcounters;

		// Token: 0x04002EB1 RID: 11953
		protected IRmsLatencyTracker rmsLatencyTracker;

		// Token: 0x020009C5 RID: 2501
		protected class LicenseState
		{
			// Token: 0x06003684 RID: 13956 RVA: 0x0008AE22 File Offset: 0x00089022
			internal LicenseState(XmlNode[] issuanceLicense, LicenseIdentity[] identities)
			{
				this.identities = identities;
				this.total = identities.Length;
				this.Responses = new LicenseResponse[this.total];
				this.IssuanceLicense = issuanceLicense;
			}

			// Token: 0x17000DE9 RID: 3561
			// (get) Token: 0x06003685 RID: 13957 RVA: 0x0008AE52 File Offset: 0x00089052
			// (set) Token: 0x06003686 RID: 13958 RVA: 0x0008AE5A File Offset: 0x0008905A
			internal LicenseResponse[] Responses { get; private set; }

			// Token: 0x17000DEA RID: 3562
			// (get) Token: 0x06003687 RID: 13959 RVA: 0x0008AE63 File Offset: 0x00089063
			// (set) Token: 0x06003688 RID: 13960 RVA: 0x0008AE6B File Offset: 0x0008906B
			internal RightsManagementException Exception { get; set; }

			// Token: 0x17000DEB RID: 3563
			// (get) Token: 0x06003689 RID: 13961 RVA: 0x0008AE74 File Offset: 0x00089074
			// (set) Token: 0x0600368A RID: 13962 RVA: 0x0008AE7C File Offset: 0x0008907C
			internal XmlNode[] IssuanceLicense { get; private set; }

			// Token: 0x0600368B RID: 13963 RVA: 0x0008AE85 File Offset: 0x00089085
			internal LicenseIdentity[] GetIdentitiesForNextBatch()
			{
				if (this.Exception != null)
				{
					return null;
				}
				this.SelectIdentitiesForNextBatch();
				if (this.identitiesSubset == null)
				{
					return null;
				}
				return this.identitiesSubset;
			}

			// Token: 0x0600368C RID: 13964 RVA: 0x0008AEA8 File Offset: 0x000890A8
			internal void SetFailureForCurrentBatch(RightsManagementException failure)
			{
				int currentBatchLength = this.GetCurrentBatchLength();
				ExTraceGlobals.RightsManagementTracer.TraceError<RightsManagementException, int>((long)this.GetHashCode(), "Setting failure {0} for the current batch. Batch length {1}", failure, currentBatchLength);
				if (currentBatchLength > 0)
				{
					int i = 0;
					int num = this.completed;
					while (i < currentBatchLength)
					{
						this.Responses[num] = new LicenseResponse(failure);
						i++;
						num++;
					}
					return;
				}
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Length ({0}) has to be more than 0.", new object[]
				{
					currentBatchLength
				}));
			}

			// Token: 0x0600368D RID: 13965 RVA: 0x0008AF24 File Offset: 0x00089124
			internal void SetFailureForCurrentBatchElement(int elementIndex, RightsManagementException failure)
			{
				int currentBatchLength = this.GetCurrentBatchLength();
				int num = this.completed + elementIndex;
				ExTraceGlobals.RightsManagementTracer.TraceError<RightsManagementException, int>((long)this.GetHashCode(), "Setting failure {0} for the batch element {1}", failure, num);
				if (currentBatchLength > 0 && num < this.total)
				{
					this.Responses[num] = new LicenseResponse(failure);
					return;
				}
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Index ({0}) has to be less than total count ({1}) and length ({2}) has to be more than 0.", new object[]
				{
					elementIndex,
					this.total,
					currentBatchLength
				}));
			}

			// Token: 0x0600368E RID: 13966 RVA: 0x0008AFB4 File Offset: 0x000891B4
			internal void SetResponseForCurrentBatchElement(int elementIndex, XmlNode[] certChain, XmlNode license)
			{
				int currentBatchLength = this.GetCurrentBatchLength();
				int num = this.completed + elementIndex;
				ExTraceGlobals.RightsManagementTracer.TraceDebug<int>((long)this.GetHashCode(), "Setting successful license for the batch element {0}", num);
				if (currentBatchLength > 0 && num < this.total)
				{
					if (string.IsNullOrEmpty(this.commonCertChain) && certChain != null && certChain.Length > 0)
					{
						if (certChain.Length > 1)
						{
							this.commonCertChain = RMUtil.ConvertXmlNodeArrayToString(certChain);
						}
						else
						{
							this.commonCertChain = certChain[0].OuterXml;
						}
					}
					string text = string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
					{
						license.OuterXml,
						this.commonCertChain
					});
					ContentRight? usageRights;
					try
					{
						usageRights = new ContentRight?(DrmClientUtils.GetUsageRightsFromLicense(text));
					}
					catch (RightsManagementException arg)
					{
						ExTraceGlobals.RightsManagementTracer.TraceError<int, RightsManagementException>((long)this.GetHashCode(), "Failed to get usage rights from license for recipient index {0}. Error {1}", num, arg);
						usageRights = null;
					}
					this.Responses[num] = new LicenseResponse(text, usageRights);
					return;
				}
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Index ({0}) has to be less than total count ({1}) and length ({2}) has to be more than 0.", new object[]
				{
					elementIndex,
					this.total,
					currentBatchLength
				}));
			}

			// Token: 0x0600368F RID: 13967 RVA: 0x0008B0F8 File Offset: 0x000892F8
			internal int GetCurrentBatchLength()
			{
				if (this.identitiesSubset == null)
				{
					return 0;
				}
				return this.identitiesSubset.Length;
			}

			// Token: 0x06003690 RID: 13968 RVA: 0x0008B10C File Offset: 0x0008930C
			protected void SelectIdentitiesForNextBatch()
			{
				this.completed += this.GetCurrentBatchLength();
				int num = this.total - this.completed;
				if (num < 1)
				{
					this.identitiesSubset = null;
					return;
				}
				int num2 = RmsAppSettings.Instance.AcquireLicenseBatchSize;
				num2 = ((num > num2) ? num2 : num);
				if (this.identitiesSubset == null || this.identitiesSubset.Length != num2)
				{
					this.identitiesSubset = new LicenseIdentity[num2];
				}
				Array.Copy(this.identities, this.completed, this.identitiesSubset, 0, num2);
			}

			// Token: 0x04002EB2 RID: 11954
			private readonly int total;

			// Token: 0x04002EB3 RID: 11955
			private int completed;

			// Token: 0x04002EB4 RID: 11956
			private LicenseIdentity[] identities;

			// Token: 0x04002EB5 RID: 11957
			private LicenseIdentity[] identitiesSubset;

			// Token: 0x04002EB6 RID: 11958
			private string commonCertChain;
		}
	}
}
