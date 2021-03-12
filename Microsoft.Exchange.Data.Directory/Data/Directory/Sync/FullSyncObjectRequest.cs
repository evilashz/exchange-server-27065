using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007EE RID: 2030
	[Serializable]
	public class FullSyncObjectRequest : ConfigurableObject
	{
		// Token: 0x0600647A RID: 25722 RVA: 0x0015C60C File Offset: 0x0015A80C
		public FullSyncObjectRequest(SyncObjectId identity, string serviceInstanceId, FullSyncObjectRequestOptions options, ExDateTime creationTime, FullSyncObjectRequestState state) : this()
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (serviceInstanceId == null)
			{
				throw new ArgumentNullException("serviceInstanceId");
			}
			this.SetIdentity(identity);
			this.ServiceInstanceId = serviceInstanceId;
			this.Options = options;
			this.CreationTime = creationTime;
			this.State = state;
		}

		// Token: 0x0600647B RID: 25723 RVA: 0x0015C666 File Offset: 0x0015A866
		public FullSyncObjectRequest() : base(new SimpleProviderPropertyBag())
		{
			base.SetExchangeVersion(ExchangeObjectVersion.Exchange2010);
			base.ResetChangeTracking();
		}

		// Token: 0x170023AA RID: 9130
		// (get) Token: 0x0600647C RID: 25724 RVA: 0x0015C684 File Offset: 0x0015A884
		// (set) Token: 0x0600647D RID: 25725 RVA: 0x0015C69B File Offset: 0x0015A89B
		public string ServiceInstanceId
		{
			get
			{
				return (string)this.propertyBag[FullSyncObjectRequestSchema.ServiceInstanceId];
			}
			set
			{
				this.propertyBag[FullSyncObjectRequestSchema.ServiceInstanceId] = value;
			}
		}

		// Token: 0x170023AB RID: 9131
		// (get) Token: 0x0600647E RID: 25726 RVA: 0x0015C6AE File Offset: 0x0015A8AE
		// (set) Token: 0x0600647F RID: 25727 RVA: 0x0015C6CF File Offset: 0x0015A8CF
		public FullSyncObjectRequestOptions Options
		{
			get
			{
				return (FullSyncObjectRequestOptions)(this.propertyBag[FullSyncObjectRequestSchema.Options] ?? FullSyncObjectRequestOptions.None);
			}
			set
			{
				this.propertyBag[FullSyncObjectRequestSchema.Options] = value;
			}
		}

		// Token: 0x170023AC RID: 9132
		// (get) Token: 0x06006480 RID: 25728 RVA: 0x0015C6E7 File Offset: 0x0015A8E7
		// (set) Token: 0x06006481 RID: 25729 RVA: 0x0015C6FE File Offset: 0x0015A8FE
		public ExDateTime CreationTime
		{
			get
			{
				return (ExDateTime)this.propertyBag[FullSyncObjectRequestSchema.CreationTime];
			}
			set
			{
				this.propertyBag[FullSyncObjectRequestSchema.CreationTime] = value;
			}
		}

		// Token: 0x170023AD RID: 9133
		// (get) Token: 0x06006482 RID: 25730 RVA: 0x0015C716 File Offset: 0x0015A916
		// (set) Token: 0x06006483 RID: 25731 RVA: 0x0015C737 File Offset: 0x0015A937
		public FullSyncObjectRequestState State
		{
			get
			{
				return (FullSyncObjectRequestState)(this.propertyBag[FullSyncObjectRequestSchema.State] ?? FullSyncObjectRequestState.New);
			}
			set
			{
				this.propertyBag[FullSyncObjectRequestSchema.State] = value;
			}
		}

		// Token: 0x170023AE RID: 9134
		// (get) Token: 0x06006484 RID: 25732 RVA: 0x0015C74F File Offset: 0x0015A94F
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return FullSyncObjectRequest.Schema;
			}
		}

		// Token: 0x06006485 RID: 25733 RVA: 0x0015C758 File Offset: 0x0015A958
		public override string ToString()
		{
			return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}", new object[]
			{
				"#",
				1,
				this.ServiceInstanceId,
				this.Identity,
				Convert.ToInt32(this.Options),
				this.CreationTime.ToFileTimeUtc(),
				Convert.ToInt32(this.State)
			});
		}

		// Token: 0x06006486 RID: 25734 RVA: 0x0015C7E0 File Offset: 0x0015A9E0
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != typeof(FullSyncObjectRequest)) && this.Equals((FullSyncObjectRequest)obj)));
		}

		// Token: 0x06006487 RID: 25735 RVA: 0x0015C820 File Offset: 0x0015AA20
		public override int GetHashCode()
		{
			return this.CreationTime.GetHashCode() ^ ((this.Identity != null) ? this.Identity.ToString().GetHashCode() : 0) ^ this.Options.GetHashCode() ^ ((this.ServiceInstanceId != null) ? this.ServiceInstanceId.GetHashCode() : 0) ^ this.State.GetHashCode();
		}

		// Token: 0x06006488 RID: 25736 RVA: 0x0015C898 File Offset: 0x0015AA98
		internal static FullSyncObjectRequest Parse(string requestBlob)
		{
			if (string.IsNullOrEmpty(requestBlob))
			{
				throw new ArgumentException("requestBlob");
			}
			FullSyncObjectRequest result;
			try
			{
				string[] array = requestBlob.Split(new string[]
				{
					"#"
				}, StringSplitOptions.None);
				if (array.Length != 6)
				{
					throw new FormatException("requestBlob");
				}
				string serviceInstanceId = array[1];
				SyncObjectId identity = SyncObjectId.Parse(array[2]);
				FullSyncObjectRequestOptions options = (FullSyncObjectRequestOptions)int.Parse(array[3]);
				ExDateTime creationTime = ExDateTime.FromFileTimeUtc(long.Parse(array[4]));
				FullSyncObjectRequestState state = (FullSyncObjectRequestState)int.Parse(array[5]);
				result = new FullSyncObjectRequest(identity, serviceInstanceId, options, creationTime, state);
			}
			catch (ArgumentException innerException)
			{
				throw new FormatException("requestBlob", innerException);
			}
			catch (FormatException innerException2)
			{
				throw new FormatException("requestBlob", innerException2);
			}
			catch (OverflowException innerException3)
			{
				throw new FormatException("requestBlob", innerException3);
			}
			return result;
		}

		// Token: 0x06006489 RID: 25737 RVA: 0x0015C97C File Offset: 0x0015AB7C
		internal bool Equals(FullSyncObjectRequest request)
		{
			return !object.ReferenceEquals(null, request) && (object.ReferenceEquals(this, request) || (this.CreationTime == request.CreationTime && this.Identity.Equals(request.Identity) && this.Options == request.Options && this.ServiceInstanceId == request.ServiceInstanceId && this.State == request.State));
		}

		// Token: 0x0600648A RID: 25738 RVA: 0x0015C9F6 File Offset: 0x0015ABF6
		internal void SetIdentity(SyncObjectId syncObjectId)
		{
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = syncObjectId;
		}

		// Token: 0x040042D2 RID: 17106
		private const int CurrentVersion = 1;

		// Token: 0x040042D3 RID: 17107
		private const string Delimiter = "#";

		// Token: 0x040042D4 RID: 17108
		private static readonly FullSyncObjectRequestSchema Schema = ObjectSchema.GetInstance<FullSyncObjectRequestSchema>();
	}
}
