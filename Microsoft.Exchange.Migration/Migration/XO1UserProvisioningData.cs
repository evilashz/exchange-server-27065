using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000D1 RID: 209
	internal class XO1UserProvisioningData : ProvisioningData
	{
		// Token: 0x06000B36 RID: 2870 RVA: 0x0002F7AA File Offset: 0x0002D9AA
		internal XO1UserProvisioningData()
		{
			base.Action = ProvisioningAction.CreateNew;
			base.ProvisioningType = ProvisioningType.XO1User;
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0002F7C1 File Offset: 0x0002D9C1
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x0002F7D3 File Offset: 0x0002D9D3
		public string FirstName
		{
			get
			{
				return (string)base["FirstName"];
			}
			private set
			{
				base["FirstName"] = value;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0002F7E1 File Offset: 0x0002D9E1
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x0002F7F3 File Offset: 0x0002D9F3
		public string LastName
		{
			get
			{
				return (string)base["LastName"];
			}
			private set
			{
				base["LastName"] = value;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0002F801 File Offset: 0x0002DA01
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x0002F813 File Offset: 0x0002DA13
		public ExTimeZone TimeZone
		{
			get
			{
				return (ExTimeZone)base["TimeZone"];
			}
			private set
			{
				base["TimeZone"] = value;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0002F821 File Offset: 0x0002DA21
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x0002F833 File Offset: 0x0002DA33
		public int LocaleId
		{
			get
			{
				return (int)base["LocaleId"];
			}
			private set
			{
				base["LocaleId"] = value;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0002F846 File Offset: 0x0002DA46
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x0002F858 File Offset: 0x0002DA58
		public string[] EmailAddresses
		{
			get
			{
				return (string[])base["EmailAddresses"];
			}
			private set
			{
				base["EmailAddresses"] = value;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0002F866 File Offset: 0x0002DA66
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x0002F878 File Offset: 0x0002DA78
		public string Database
		{
			get
			{
				return (string)base["Database"];
			}
			private set
			{
				base["Database"] = value;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0002F886 File Offset: 0x0002DA86
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x0002F898 File Offset: 0x0002DA98
		public bool MakeExoSecondary
		{
			get
			{
				return (bool)base["MakeExoSecondary"];
			}
			private set
			{
				base["MakeExoSecondary"] = value;
			}
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0002F8AC File Offset: 0x0002DAAC
		public static XO1UserProvisioningData Create(string identity, long puid, ExTimeZone timeZone, int localeId, string database, string firstName, string lastName, string[] aliases)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(identity, "identity");
			MigrationUtil.AssertOrThrow(puid != 0L, "invalid puid", new object[0]);
			MigrationUtil.ThrowOnNullArgument(timeZone, "timeZone");
			MigrationUtil.ThrowOnNullOrEmptyArgument(database, "database");
			XO1UserProvisioningData xo1UserProvisioningData = new XO1UserProvisioningData();
			xo1UserProvisioningData.Identity = new NetID(puid).ToString();
			xo1UserProvisioningData.TimeZone = timeZone;
			xo1UserProvisioningData.LocaleId = localeId;
			xo1UserProvisioningData.Database = database;
			xo1UserProvisioningData.MakeExoSecondary = true;
			if (!string.IsNullOrEmpty(firstName))
			{
				xo1UserProvisioningData.FirstName = firstName;
			}
			if (!string.IsNullOrEmpty(lastName))
			{
				xo1UserProvisioningData.LastName = lastName;
			}
			List<string> list = new List<string>();
			if (aliases != null)
			{
				list.AddRange(aliases);
			}
			list.Add(identity);
			if (list.Count > 0)
			{
				xo1UserProvisioningData.EmailAddresses = list.ToArray();
			}
			return xo1UserProvisioningData;
		}

		// Token: 0x04000447 RID: 1095
		public const string FirstNameParameterName = "FirstName";

		// Token: 0x04000448 RID: 1096
		public const string LastNameParameterName = "LastName";

		// Token: 0x04000449 RID: 1097
		public const string TimeZoneParameterName = "TimeZone";

		// Token: 0x0400044A RID: 1098
		public const string LocaleIdParameterName = "LocaleId";

		// Token: 0x0400044B RID: 1099
		public const string EmailAddressesParameterName = "EmailAddresses";

		// Token: 0x0400044C RID: 1100
		public const string DatabaseParameterName = "Database";

		// Token: 0x0400044D RID: 1101
		public const string MakeExoSecondaryParameterName = "MakeExoSecondary";
	}
}
