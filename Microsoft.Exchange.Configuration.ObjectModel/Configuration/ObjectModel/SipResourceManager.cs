using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000266 RID: 614
	internal class SipResourceManager : ResourceManager
	{
		// Token: 0x0600154B RID: 5451 RVA: 0x0004EBD1 File Offset: 0x0004CDD1
		internal SipResourceManager(string baseName, Assembly assembly) : base(baseName, assembly)
		{
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0004EBDB File Offset: 0x0004CDDB
		public override string GetString(string name)
		{
			return this.GetString(name, CultureInfo.CurrentUICulture);
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0004EBEC File Offset: 0x0004CDEC
		public override string GetString(string name, CultureInfo culture)
		{
			SipCultureInfoBase sipCultureInfoBase = culture as SipCultureInfoBase;
			if (sipCultureInfoBase != null)
			{
				try
				{
					sipCultureInfoBase.UseSipName = true;
					return base.GetString(name, sipCultureInfoBase);
				}
				finally
				{
					sipCultureInfoBase.UseSipName = false;
				}
			}
			return base.GetString(name, culture);
		}
	}
}
