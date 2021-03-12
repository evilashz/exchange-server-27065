using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000358 RID: 856
	[DataContract]
	public class SecurityPrincipalRow : AdObjectResolverRow
	{
		// Token: 0x06002FB2 RID: 12210 RVA: 0x00091566 File Offset: 0x0008F766
		public SecurityPrincipalRow(ADRawEntry aDRawEntry) : base(aDRawEntry)
		{
		}

		// Token: 0x17001F10 RID: 7952
		// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x0009156F File Offset: 0x0008F76F
		// (set) Token: 0x06002FB4 RID: 12212 RVA: 0x00091586 File Offset: 0x0008F786
		[DataMember]
		public string Name
		{
			get
			{
				return (string)base.ADRawEntry[ADObjectSchema.Name];
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001F11 RID: 7953
		// (get) Token: 0x06002FB5 RID: 12213 RVA: 0x00091590 File Offset: 0x0008F790
		public override string DisplayName
		{
			get
			{
				string text = (string)base.ADRawEntry[ExtendedSecurityPrincipalSchema.DisplayName];
				if (string.IsNullOrEmpty(text))
				{
					text = this.Name;
				}
				return text;
			}
		}

		// Token: 0x17001F12 RID: 7954
		// (get) Token: 0x06002FB6 RID: 12214 RVA: 0x000915C3 File Offset: 0x0008F7C3
		// (set) Token: 0x06002FB7 RID: 12215 RVA: 0x000915DF File Offset: 0x0008F7DF
		[DataMember]
		public string SpriteId
		{
			get
			{
				return Icons.FromEnum((SecurityPrincipalType)base.ADRawEntry[ExtendedSecurityPrincipalSchema.SecurityPrincipalTypes]);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001F13 RID: 7955
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x000915E6 File Offset: 0x0008F7E6
		// (set) Token: 0x06002FB9 RID: 12217 RVA: 0x00091602 File Offset: 0x0008F802
		[DataMember]
		public string IconAltText
		{
			get
			{
				return Icons.GenerateIconAltText((SecurityPrincipalType)base.ADRawEntry[ExtendedSecurityPrincipalSchema.SecurityPrincipalTypes]);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04002317 RID: 8983
		public new static PropertyDefinition[] Properties = new List<PropertyDefinition>(AdObjectResolverRow.Properties)
		{
			ExtendedSecurityPrincipalSchema.DisplayName,
			ADObjectSchema.ObjectClass,
			ExtendedSecurityPrincipalSchema.SecurityPrincipalTypes
		}.ToArray();
	}
}
