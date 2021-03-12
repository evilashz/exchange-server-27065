using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ABProviderFramework
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ABObject : ABRawEntry
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000022B6 File Offset: 0x000004B6
		protected ABObject(ABSession ownerSession, ABPropertyDefinitionCollection properties) : base(ownerSession, properties)
		{
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000022C0 File Offset: 0x000004C0
		public ABObjectId Id
		{
			get
			{
				return (ABObjectId)this[ABObjectSchema.Id];
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12
		public abstract ABObjectSchema Schema { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000022D2 File Offset: 0x000004D2
		public bool CanEmail
		{
			get
			{
				return (bool)this[ABObjectSchema.CanEmail];
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000022E4 File Offset: 0x000004E4
		public string LegacyExchangeDN
		{
			get
			{
				return (string)this[ABObjectSchema.LegacyExchangeDN];
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000022F6 File Offset: 0x000004F6
		public string DisplayName
		{
			get
			{
				return (string)this[ABObjectSchema.DisplayName];
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002308 File Offset: 0x00000508
		public string Alias
		{
			get
			{
				return (string)this[ABObjectSchema.Alias];
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000231A File Offset: 0x0000051A
		public string EmailAddress
		{
			get
			{
				return (string)this[ABObjectSchema.EmailAddress];
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000232C File Offset: 0x0000052C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(base.GetType().Name + " = {");
			if (this.Id == null)
			{
				stringBuilder.AppendLine("  Id = <null>");
			}
			else
			{
				stringBuilder.AppendLine("  Id = " + this.Id.ToString());
			}
			if (!string.IsNullOrEmpty(this.DisplayName))
			{
				stringBuilder.AppendLine("  DisplayName = " + this.DisplayName);
			}
			if (!string.IsNullOrEmpty(this.LegacyExchangeDN))
			{
				stringBuilder.AppendLine("  LegacyExchangeDN = " + this.LegacyExchangeDN);
			}
			if (!string.IsNullOrEmpty(this.EmailAddress))
			{
				stringBuilder.AppendLine("  EmailAddress = " + this.EmailAddress);
			}
			stringBuilder.AppendLine("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000240C File Offset: 0x0000060C
		protected override bool InternalTryGetValue(ABPropertyDefinition property, out object value)
		{
			if (property == ABObjectSchema.Alias)
			{
				value = this.GetAlias();
				return true;
			}
			if (property == ABObjectSchema.DisplayName)
			{
				value = this.GetDisplayName();
				return true;
			}
			if (property == ABObjectSchema.LegacyExchangeDN)
			{
				value = this.GetLegacyExchangeDN();
				return true;
			}
			if (property == ABObjectSchema.CanEmail)
			{
				value = this.GetCanEmail();
				return true;
			}
			if (property == ABObjectSchema.Id)
			{
				value = this.GetId();
				return true;
			}
			if (property == ABObjectSchema.EmailAddress)
			{
				value = this.GetEmailAddress();
				return true;
			}
			return base.InternalTryGetValue(property, out value);
		}

		// Token: 0x06000014 RID: 20
		protected abstract string GetAlias();

		// Token: 0x06000015 RID: 21
		protected abstract string GetDisplayName();

		// Token: 0x06000016 RID: 22
		protected abstract string GetLegacyExchangeDN();

		// Token: 0x06000017 RID: 23
		protected abstract bool GetCanEmail();

		// Token: 0x06000018 RID: 24
		protected abstract ABObjectId GetId();

		// Token: 0x06000019 RID: 25
		protected abstract string GetEmailAddress();
	}
}
