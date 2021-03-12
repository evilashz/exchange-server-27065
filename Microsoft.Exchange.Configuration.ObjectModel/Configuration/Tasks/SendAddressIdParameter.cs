using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000145 RID: 325
	[Serializable]
	public sealed class SendAddressIdParameter : IIdentityParameter
	{
		// Token: 0x06000B9A RID: 2970 RVA: 0x00024E46 File Offset: 0x00023046
		public SendAddressIdParameter()
		{
			this.sendAddressIdentity = new SendAddressIdentity();
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00024E59 File Offset: 0x00023059
		public SendAddressIdParameter(SendAddress sendAddress)
		{
			this.Initialize(sendAddress.Identity);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00024E6D File Offset: 0x0002306D
		public SendAddressIdParameter(string stringIdentity)
		{
			this.ThrowIfArgumentNullOrEmpty("stringIdentity", stringIdentity);
			this.Parse(stringIdentity);
			this.mailboxIdParameter = this.FromSendAddressIdentity();
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00024E94 File Offset: 0x00023094
		internal SendAddressIdParameter(SendAddressIdentity sendAddressIdentity)
		{
			this.Initialize(sendAddressIdentity);
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00024EA3 File Offset: 0x000230A3
		public string RawIdentity
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00024EAB File Offset: 0x000230AB
		public bool IsUniqueIdentity
		{
			get
			{
				return this.sendAddressIdentity.IsUniqueIdentity;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x00024EB8 File Offset: 0x000230B8
		public MailboxIdParameter MailboxIdParameter
		{
			get
			{
				return this.mailboxIdParameter;
			}
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00024EC0 File Offset: 0x000230C0
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			notFoundReason = null;
			if (this.IsUniqueIdentity)
			{
				return new List<T>(1)
				{
					(T)((object)session.Read<T>(this.sendAddressIdentity))
				};
			}
			return session.FindPaged<T>(null, rootId, false, null, 0);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00024F08 File Offset: 0x00023108
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return this.GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00024F20 File Offset: 0x00023120
		public void Initialize(ObjectId objectId)
		{
			this.ThrowIfArgumentNull("objectId", objectId);
			SendAddressIdentity sendAddressIdentity = objectId as SendAddressIdentity;
			if (sendAddressIdentity == null)
			{
				string message = string.Format("objectId is the wrong type: {0} expected: {1}", objectId.GetType().Name, typeof(SendAddressIdentity).Name, CultureInfo.InvariantCulture);
				throw new ArgumentException(message, "objectId");
			}
			this.sendAddressIdentity = sendAddressIdentity;
			this.mailboxIdParameter = this.FromSendAddressIdentity();
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00024F8C File Offset: 0x0002318C
		public override string ToString()
		{
			if (this.sendAddressIdentity != null)
			{
				return this.sendAddressIdentity.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00024FA8 File Offset: 0x000231A8
		private void Parse(string stringIdentity)
		{
			string text = stringIdentity.Trim();
			if (text.Length == 0)
			{
				throw new ArgumentException("stringIdentity contained only spaces", "stringIdentity");
			}
			this.sendAddressIdentity = new SendAddressIdentity(text);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00024FE0 File Offset: 0x000231E0
		private MailboxIdParameter FromSendAddressIdentity()
		{
			return new MailboxIdParameter(this.sendAddressIdentity.MailboxIdParameterString);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00024FF2 File Offset: 0x000231F2
		private void ThrowIfArgumentNull(string name, object argument)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00024FFE File Offset: 0x000231FE
		private void ThrowIfArgumentNullOrEmpty(string name, string argument)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(name);
			}
			if (argument.Length == 0)
			{
				throw new ArgumentException("The value is set to empty", name);
			}
		}

		// Token: 0x040002AA RID: 682
		private SendAddressIdentity sendAddressIdentity;

		// Token: 0x040002AB RID: 683
		private MailboxIdParameter mailboxIdParameter;
	}
}
