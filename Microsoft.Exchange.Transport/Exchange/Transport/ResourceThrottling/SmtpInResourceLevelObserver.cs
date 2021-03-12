using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport.ResourceThrottling
{
	// Token: 0x02000056 RID: 86
	internal class SmtpInResourceLevelObserver : IResourceLevelObserver
	{
		// Token: 0x06000234 RID: 564 RVA: 0x0000B078 File Offset: 0x00009278
		public SmtpInResourceLevelObserver(ISmtpInComponent smtpInComponent, ThrottlingController throttlingController, IComponentsWrapper componentsWrapper)
		{
			ArgumentValidator.ThrowIfNull("smtpInComponent", smtpInComponent);
			ArgumentValidator.ThrowIfNull("throttlingController", throttlingController);
			ArgumentValidator.ThrowIfNull("componentsWrapper", componentsWrapper);
			this.smtpInComponent = smtpInComponent;
			this.throttlingController = throttlingController;
			this.componentsWrapper = componentsWrapper;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000B100 File Offset: 0x00009300
		public ISmtpInComponent SmtpInComponent
		{
			get
			{
				return this.smtpInComponent;
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000B108 File Offset: 0x00009308
		public virtual void HandleResourceChange(IEnumerable<ResourceUse> allResourceUses, IEnumerable<ResourceUse> changedResourceUses, IEnumerable<ResourceUse> rawResourceUses)
		{
			ArgumentValidator.ThrowIfNull("allResourceUses", allResourceUses);
			ArgumentValidator.ThrowIfNull("changedResourceUses", changedResourceUses);
			ArgumentValidator.ThrowIfNull("rawResourceUses", rawResourceUses);
			this.SetThrottleDelay(rawResourceUses);
			if (this.componentsWrapper.IsPaused)
			{
				return;
			}
			UseLevel useLevel = ResourceHelper.TryGetCurrentUseLevel(allResourceUses, this.aggregateResourceIdentifier, UseLevel.Low);
			if (useLevel == UseLevel.Low)
			{
				this.smtpInComponent.Continue();
				this.componentPaused = false;
				this.rejectSubmits = false;
				return;
			}
			this.rejectSubmits = (useLevel == UseLevel.High);
			this.smtpInComponent.Pause(this.rejectSubmits, SmtpResponse.InsufficientResource);
			this.componentPaused = true;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000B19D File Offset: 0x0000939D
		public string Name
		{
			get
			{
				return "SmtpIn";
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000B1A4 File Offset: 0x000093A4
		private void SetThrottleDelay(IEnumerable<ResourceUse> rawResourceUses)
		{
			UseLevel useLevel = ResourceHelper.TryGetCurrentUseLevel(rawResourceUses, this.submissionQueueResource, UseLevel.Low);
			UseLevel useLevel2 = ResourceHelper.TryGetCurrentUseLevel(rawResourceUses, this.versionBucketsResource, UseLevel.Low);
			if (useLevel != UseLevel.Low || useLevel2 != UseLevel.Low)
			{
				this.throttlingController.Increase();
			}
			else
			{
				this.throttlingController.Decrease();
			}
			TimeSpan current = this.throttlingController.GetCurrent();
			if (current == TimeSpan.Zero)
			{
				this.smtpInComponent.SetThrottleDelay(current, null);
				return;
			}
			string throttleDelayContext = string.Format(CultureInfo.InvariantCulture, "VB={0};QS={1}", new object[]
			{
				useLevel2,
				useLevel
			});
			this.smtpInComponent.SetThrottleDelay(current, throttleDelayContext);
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000B24D File Offset: 0x0000944D
		public bool Paused
		{
			get
			{
				return this.componentPaused;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000B255 File Offset: 0x00009455
		public string SubStatus
		{
			get
			{
				if (this.rejectSubmits)
				{
					return "Rejecting Submissions";
				}
				return string.Empty;
			}
		}

		// Token: 0x04000163 RID: 355
		internal const string RejectSubmitStatus = "Rejecting Submissions";

		// Token: 0x04000164 RID: 356
		internal const string ResourceObserverName = "SmtpIn";

		// Token: 0x04000165 RID: 357
		private readonly ISmtpInComponent smtpInComponent;

		// Token: 0x04000166 RID: 358
		private readonly IComponentsWrapper componentsWrapper;

		// Token: 0x04000167 RID: 359
		private readonly ThrottlingController throttlingController;

		// Token: 0x04000168 RID: 360
		private readonly ResourceIdentifier submissionQueueResource = new ResourceIdentifier("QueueLength", "SubmissionQueue");

		// Token: 0x04000169 RID: 361
		private readonly ResourceIdentifier versionBucketsResource = new ResourceIdentifier("UsedVersionBuckets", "");

		// Token: 0x0400016A RID: 362
		private readonly ResourceIdentifier aggregateResourceIdentifier = new ResourceIdentifier("Aggregate", "");

		// Token: 0x0400016B RID: 363
		private bool componentPaused;

		// Token: 0x0400016C RID: 364
		private bool rejectSubmits;
	}
}
