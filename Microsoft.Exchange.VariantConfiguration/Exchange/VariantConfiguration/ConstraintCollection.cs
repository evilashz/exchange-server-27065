using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using Microsoft.Exchange.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x02000004 RID: 4
	public class ConstraintCollection : Dictionary<string, string>
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000212F File Offset: 0x0000032F
		public ConstraintCollection(ConstraintCollection collection) : base(collection, StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000213D File Offset: 0x0000033D
		private ConstraintCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000214C File Offset: 0x0000034C
		internal static string Mode
		{
			get
			{
				if (ConstraintCollection.mode == null)
				{
					switch (Datacenter.GetExchangeSku())
					{
					case Datacenter.ExchangeSku.Enterprise:
						ConstraintCollection.mode = "enterprise";
						goto IL_59;
					case Datacenter.ExchangeSku.ExchangeDatacenter:
						ConstraintCollection.mode = "datacenter";
						goto IL_59;
					case Datacenter.ExchangeSku.DatacenterDedicated:
						ConstraintCollection.mode = "dedicated";
						goto IL_59;
					}
					ConstraintCollection.mode = string.Empty;
				}
				IL_59:
				return ConstraintCollection.mode;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021B8 File Offset: 0x000003B8
		private static ConstraintCollection GlobalConstraints
		{
			get
			{
				if (ConstraintCollection.globalConstraints == null)
				{
					ConstraintCollection constraintCollection = new ConstraintCollection();
					if (!string.IsNullOrEmpty(ConstraintCollection.Mode))
					{
						constraintCollection.Add(VariantType.Mode, ConstraintCollection.Mode);
					}
					constraintCollection.Add(VariantType.Machine, Environment.MachineName);
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\VariantConfiguration", false))
					{
						if (registryKey != null)
						{
							foreach (VariantType variantType in ConstraintCollection.RegistryValueNames)
							{
								object value = registryKey.GetValue(variantType.Name);
								if (value != null)
								{
									constraintCollection.Add(variantType.Name, value.ToString());
								}
							}
						}
					}
					if (ConstraintCollection.Mode.Equals("datacenter"))
					{
						string region = ConstraintCollection.GetRegion();
						if (!string.IsNullOrEmpty(region))
						{
							constraintCollection.Add(VariantType.Region, region);
						}
					}
					constraintCollection.Add(VariantType.Process, ConstraintCollection.GetProcessName());
					lock (ConstraintCollection.lockGlobalConstraints)
					{
						if (ConstraintCollection.globalConstraints == null)
						{
							ConstraintCollection.globalConstraints = constraintCollection;
						}
					}
				}
				return ConstraintCollection.globalConstraints;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022F0 File Offset: 0x000004F0
		public static void SetGlobalConstraint(string variant, string value)
		{
			lock (ConstraintCollection.lockGlobalConstraints)
			{
				ConstraintCollection.globalConstraints = new ConstraintCollection(ConstraintCollection.GlobalConstraints)
				{
					{
						variant,
						value
					}
				};
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002344 File Offset: 0x00000544
		public static ConstraintCollection CreateEmpty()
		{
			return new ConstraintCollection();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000234B File Offset: 0x0000054B
		public static ConstraintCollection CreateGlobal()
		{
			return new ConstraintCollection(ConstraintCollection.GlobalConstraints);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002358 File Offset: 0x00000558
		public void Add(ConstraintCollection constraints)
		{
			foreach (KeyValuePair<string, string> keyValuePair in constraints)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023B4 File Offset: 0x000005B4
		public void Add(VariantType variant, string value)
		{
			this.Add(variant.Name, value);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023C3 File Offset: 0x000005C3
		public void Add(VariantType variant, bool value)
		{
			this.Add(variant.Name, value);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023D2 File Offset: 0x000005D2
		public void Add(VariantType variant, Guid value)
		{
			this.Add(variant.Name, value.ToString());
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023F0 File Offset: 0x000005F0
		public void Add(VariantType variant, Version value)
		{
			this.Add(variant.Name, string.Format("{0}.{1:00}.{2:0000}.{3:000}", new object[]
			{
				value.Major,
				value.Minor,
				value.Build,
				value.Revision
			}));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002453 File Offset: 0x00000653
		public new void Add(string key, string value)
		{
			base[key] = value;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000245D File Offset: 0x0000065D
		public void Add(string key, bool value)
		{
			if (value)
			{
				this.Add(key, bool.TrueString);
				return;
			}
			base.Remove(key);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002478 File Offset: 0x00000678
		private static string GetProcessName()
		{
			string fileNameWithoutExtension;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				fileNameWithoutExtension = Path.GetFileNameWithoutExtension(currentProcess.ProcessName);
			}
			return fileNameWithoutExtension;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024B4 File Offset: 0x000006B4
		private static string GetRegion()
		{
			string domainName;
			try
			{
				IPGlobalProperties ipglobalProperties = IPGlobalProperties.GetIPGlobalProperties();
				if (ipglobalProperties == null)
				{
					return null;
				}
				domainName = ipglobalProperties.DomainName;
			}
			catch (NetworkInformationException)
			{
				return null;
			}
			if (domainName.Length < 3)
			{
				return null;
			}
			if (domainName.Equals("prod.exchangelabs.com", StringComparison.OrdinalIgnoreCase))
			{
				return "nam";
			}
			return domainName.Substring(0, 3);
		}

		// Token: 0x04000003 RID: 3
		internal const string ModeEnterprise = "enterprise";

		// Token: 0x04000004 RID: 4
		internal const string ModeDedicated = "dedicated";

		// Token: 0x04000005 RID: 5
		internal const string ModeDatacenter = "datacenter";

		// Token: 0x04000006 RID: 6
		private const string VariantConfigurationKeyBasePath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\VariantConfiguration";

		// Token: 0x04000007 RID: 7
		private static readonly VariantType[] RegistryValueNames = new VariantType[]
		{
			VariantType.Dag,
			VariantType.Forest,
			VariantType.Primary,
			VariantType.Pod,
			VariantType.Service,
			VariantType.Test
		};

		// Token: 0x04000008 RID: 8
		private static string mode;

		// Token: 0x04000009 RID: 9
		private static ConstraintCollection globalConstraints;

		// Token: 0x0400000A RID: 10
		private static object lockGlobalConstraints = new object();
	}
}
