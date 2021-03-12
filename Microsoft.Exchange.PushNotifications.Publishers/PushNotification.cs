using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000009 RID: 9
	internal abstract class PushNotification
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000024EC File Offset: 0x000006EC
		protected PushNotification(string appId, OrganizationId tenantId)
		{
			this.AppId = appId;
			this.TenantId = tenantId;
			this.SequenceNumber = PushNotification.GetNextId();
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000250D File Offset: 0x0000070D
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002515 File Offset: 0x00000715
		public string AppId { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000251E File Offset: 0x0000071E
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002526 File Offset: 0x00000726
		public int SequenceNumber { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000252F File Offset: 0x0000072F
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002537 File Offset: 0x00000737
		public OrganizationId TenantId { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002540 File Offset: 0x00000740
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002548 File Offset: 0x00000748
		public bool IsBackgroundSyncAvailable { get; protected set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002551 File Offset: 0x00000751
		public string Identifier
		{
			get
			{
				if (this.identifier == null)
				{
					this.identifier = string.Format(PushNotification.IdTemplate, this.SequenceNumber.ToNullableString(null));
				}
				return this.identifier;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000026 RID: 38
		public abstract string RecipientId { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000257D File Offset: 0x0000077D
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000259D File Offset: 0x0000079D
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

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000025B8 File Offset: 0x000007B8
		public virtual bool IsMonitoring
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025BB File Offset: 0x000007BB
		public void Validate()
		{
			if (!this.IsValid)
			{
				throw new InvalidPushNotificationException(this.validationErrors[0]);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025D7 File Offset: 0x000007D7
		public sealed override string ToString()
		{
			if (this.toStringCache == null)
			{
				this.toStringCache = "id:" + this.Identifier.ToNullableString();
			}
			return this.toStringCache;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002602 File Offset: 0x00000802
		public string ToFullString()
		{
			if (this.toFullStringCache == null)
			{
				this.toFullStringCache = string.Format("{{{0}}}", this.InternalToFullString());
			}
			return this.toFullStringCache;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002628 File Offset: 0x00000828
		protected virtual string InternalToFullString()
		{
			return string.Format("appId:{0}; tenantId:{1}; id:{2}; isMonitoring:{3}", new object[]
			{
				this.AppId.ToNullableString(),
				this.TenantId.ToNullableString(null),
				this.Identifier.ToNullableString(),
				this.IsMonitoring.ToString()
			});
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002684 File Offset: 0x00000884
		protected virtual void RunValidationCheck(List<LocalizedString> errors)
		{
			if (ExTraceGlobals.NotificationFormatTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.NotificationFormatTracer.TraceDebug<string>((long)this.GetHashCode(), "[PushNotification::RunValidationCheck] Validating notification '{0}'", this.ToFullString());
			}
			if (string.IsNullOrWhiteSpace(this.AppId))
			{
				errors.Add(Strings.InvalidAppId);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026D2 File Offset: 0x000008D2
		private static int GetNextId()
		{
			return Interlocked.Increment(ref PushNotification.idCounter);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026E0 File Offset: 0x000008E0
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

		// Token: 0x04000008 RID: 8
		private static readonly string IdTemplate = ExDateTime.UtcNow.ToString("yyyyMMdd-HHmmss-{0}");

		// Token: 0x04000009 RID: 9
		private static readonly char[] IdSeparator = new char[]
		{
			'-'
		};

		// Token: 0x0400000A RID: 10
		private static int idCounter;

		// Token: 0x0400000B RID: 11
		private string identifier;

		// Token: 0x0400000C RID: 12
		private string toStringCache;

		// Token: 0x0400000D RID: 13
		private string toFullStringCache;

		// Token: 0x0400000E RID: 14
		private bool? isValid;

		// Token: 0x0400000F RID: 15
		private List<LocalizedString> validationErrors;
	}
}
