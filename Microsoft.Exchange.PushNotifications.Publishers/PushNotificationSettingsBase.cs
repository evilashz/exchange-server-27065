using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200001F RID: 31
	internal abstract class PushNotificationSettingsBase
	{
		// Token: 0x06000127 RID: 295 RVA: 0x000057D4 File Offset: 0x000039D4
		public PushNotificationSettingsBase(string appId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("appId", appId);
			this.AppId = appId;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000057EE File Offset: 0x000039EE
		// (set) Token: 0x06000129 RID: 297 RVA: 0x000057F6 File Offset: 0x000039F6
		public string AppId { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000057FF File Offset: 0x000039FF
		public bool IsSuitable
		{
			get
			{
				if (this.isSuitable == null)
				{
					this.RunBaseSuitabilityCheck();
				}
				return this.isSuitable.Value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000581F File Offset: 0x00003A1F
		public bool IsValid
		{
			get
			{
				if (this.isValid == null)
				{
					this.RunBaseValidationCheck();
				}
				return this.isValid.Value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000583F File Offset: 0x00003A3F
		public List<LocalizedString> ValidationErrors
		{
			get
			{
				if (!this.IsValid)
				{
					return this.validationErrors;
				}
				throw new InvalidOperationException("ValidationErrors are not available when the instance is valid");
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000585A File Offset: 0x00003A5A
		public void Validate()
		{
			if (!this.IsValid)
			{
				throw new PushNotificationConfigurationException(this.validationErrors[0]);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005876 File Offset: 0x00003A76
		protected virtual void RunValidationCheck(List<LocalizedString> errors)
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005878 File Offset: 0x00003A78
		protected virtual bool RunSuitabilityCheck()
		{
			return true;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000587C File Offset: 0x00003A7C
		private void RunBaseValidationCheck()
		{
			List<LocalizedString> list = new List<LocalizedString>();
			this.RunValidationCheck(list);
			if (list.Count == 0)
			{
				this.isValid = new bool?(true);
				return;
			}
			this.validationErrors = list;
			this.isValid = new bool?(false);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000058C0 File Offset: 0x00003AC0
		private void RunBaseSuitabilityCheck()
		{
			bool value;
			if (this.IsValid)
			{
				value = this.RunSuitabilityCheck();
			}
			else
			{
				string text = string.Join<LocalizedString>("\n\r", this.validationErrors.ToArray());
				PushNotificationsCrimsonEvents.PushNotificationPublisherConfigurationError.Log<string, string, string>(this.AppId, string.Empty, text);
				ExTraceGlobals.PublisherManagerTracer.TraceError<string, string>((long)this.GetHashCode(), "[PushNotificationSettingsBase:RunBaseSuitabilityCheck] Configuration for '{0}' has validation errors: {1}", this.AppId, text);
				value = false;
			}
			this.isSuitable = new bool?(value);
		}

		// Token: 0x0400005F RID: 95
		private bool? isValid;

		// Token: 0x04000060 RID: 96
		private List<LocalizedString> validationErrors;

		// Token: 0x04000061 RID: 97
		private bool? isSuitable;
	}
}
