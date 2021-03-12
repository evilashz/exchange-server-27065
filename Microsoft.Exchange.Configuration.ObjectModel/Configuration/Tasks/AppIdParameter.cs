using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000154 RID: 340
	[Serializable]
	public class AppIdParameter : IIdentityParameter
	{
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x000269EF File Offset: 0x00024BEF
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.rawInput;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x000269F7 File Offset: 0x00024BF7
		// (set) Token: 0x06000C40 RID: 3136 RVA: 0x000269FF File Offset: 0x00024BFF
		internal AppId InternalOWAExtensionId
		{
			get
			{
				return this.internalOWAExtensionId;
			}
			set
			{
				this.internalOWAExtensionId = value;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x00026A08 File Offset: 0x00024C08
		internal string RawExtensionName
		{
			get
			{
				return this.rawExtensionName;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00026A10 File Offset: 0x00024C10
		internal MailboxIdParameter RawMailbox
		{
			get
			{
				return this.rawMailbox;
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00026A18 File Offset: 0x00024C18
		public AppIdParameter()
		{
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00026A20 File Offset: 0x00024C20
		public AppIdParameter(ConfigurableObject configurableObject)
		{
			if (configurableObject == null)
			{
				throw new ArgumentNullException("configurableObject");
			}
			((IIdentityParameter)this).Initialize(configurableObject.Identity);
			this.rawInput = configurableObject.Identity.ToString();
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00026A53 File Offset: 0x00024C53
		public AppIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
			if (string.IsNullOrEmpty(this.rawExtensionName))
			{
				this.rawExtensionName = namedIdentity.DisplayName;
			}
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00026A7C File Offset: 0x00024C7C
		public AppIdParameter(string extensionId)
		{
			if (string.IsNullOrEmpty(extensionId))
			{
				throw new ArgumentNullException("extensionId");
			}
			this.rawInput = extensionId;
			string text = string.Empty;
			int num = extensionId.Length;
			int num2;
			do
			{
				num2 = num;
				num = extensionId.LastIndexOf("\\\\", num2 - 1, num2);
			}
			while (num != -1);
			int num3 = extensionId.LastIndexOf('\\', num2 - 1, num2);
			if (num3 == extensionId.Length - 1)
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterFormat("extensionId"), "extensionId");
			}
			string text2;
			if (num3 >= 0)
			{
				text2 = extensionId.Substring(0, num3);
				text = extensionId.Substring(1 + num3);
			}
			else
			{
				text = extensionId;
				text2 = null;
			}
			if (num2 != extensionId.Length)
			{
				text = text.Replace("\\\\", '\\'.ToString());
			}
			if (!string.IsNullOrEmpty(text2) && !text2.Equals(Guid.Empty.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				this.rawMailbox = new MailboxIdParameter(text2);
			}
			this.rawExtensionName = text;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00026B78 File Offset: 0x00024D78
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			LocalizedString? localizedString;
			return ((IIdentityParameter)this).GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00026B90 File Offset: 0x00024D90
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (null == this.internalOWAExtensionId)
			{
				throw new InvalidOperationException(Strings.ErrorOperationOnInvalidObject);
			}
			IConfigurable[] array = session.Find<T>(null, this.internalOWAExtensionId, false, null);
			if (array == null || array.Length == 0)
			{
				notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFound(this.ToString()));
				return new T[0];
			}
			notFoundReason = null;
			T[] array2 = new T[array.Length];
			int num = 0;
			foreach (IConfigurable configurable in array)
			{
				array2[num++] = (T)((object)configurable);
			}
			return array2;
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00026C40 File Offset: 0x00024E40
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			if (!(objectId is AppId))
			{
				throw new NotSupportedException("objectId: " + objectId.GetType().FullName);
			}
			this.internalOWAExtensionId = (AppId)objectId;
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00026C7F File Offset: 0x00024E7F
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.RawExtensionName))
			{
				return this.RawExtensionName;
			}
			if (this.InternalOWAExtensionId != null)
			{
				return this.InternalOWAExtensionId.ToString();
			}
			return this.rawInput;
		}

		// Token: 0x040002D1 RID: 721
		private readonly MailboxIdParameter rawMailbox;

		// Token: 0x040002D2 RID: 722
		private readonly string rawInput;

		// Token: 0x040002D3 RID: 723
		private readonly string rawExtensionName;

		// Token: 0x040002D4 RID: 724
		private AppId internalOWAExtensionId;
	}
}
