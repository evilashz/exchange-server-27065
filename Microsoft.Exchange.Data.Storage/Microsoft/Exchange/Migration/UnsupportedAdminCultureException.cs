using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000146 RID: 326
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnsupportedAdminCultureException : MigrationPermanentException
	{
		// Token: 0x060015C9 RID: 5577 RVA: 0x0006E751 File Offset: 0x0006C951
		public UnsupportedAdminCultureException(string culture) : base(Strings.UnsupportedAdminCulture(culture))
		{
			this.culture = culture;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x0006E766 File Offset: 0x0006C966
		public UnsupportedAdminCultureException(string culture, Exception innerException) : base(Strings.UnsupportedAdminCulture(culture), innerException)
		{
			this.culture = culture;
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0006E77C File Offset: 0x0006C97C
		protected UnsupportedAdminCultureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.culture = (string)info.GetValue("culture", typeof(string));
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0006E7A6 File Offset: 0x0006C9A6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("culture", this.culture);
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x0006E7C1 File Offset: 0x0006C9C1
		public string Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x04000AD9 RID: 2777
		private readonly string culture;
	}
}
