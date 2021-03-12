using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Internal.ManagedWPP;

namespace Microsoft.Forefront.ActiveDirectoryConnector
{
	// Token: 0x02000005 RID: 5
	internal class ADHelpers
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002B35 File Offset: 0x00000D35
		static ADHelpers()
		{
			Globals.InitializeSinglePerfCounterInstance();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002B46 File Offset: 0x00000D46
		public static ITopologyConfigurationSession CreateDefaultReadOnlyTopologyConfigurationSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 47, "CreateDefaultReadOnlyTopologyConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Filtering\\src\\platform\\Management\\ADConnector\\impl\\obj\\amd64\\adhelpers.cs");
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002D88 File Offset: 0x00000F88
		public static IEnumerable<ADHelpers.ADRulePackageInfo> GetClassificationRulePackages(string rulePackageSetId, IEnumerable<string> rulePackageIds)
		{
			IConfigurationSession configSession = ADHelpers.GetConfigurationSession(rulePackageSetId);
			foreach (TransportRule rule in ADHelpers.GetRulePackages(configSession, rulePackageIds.ToArray<string>()))
			{
				yield return new ADHelpers.ADRulePackageInfo
				{
					RulePackageSetId = rulePackageSetId,
					RulePackageId = rule.Name,
					Xml = ADHelpers.DecodeClassificationRules(rule.ReplicationSignature),
					ModifiedDate = rule.WhenChangedUTC.Value
				};
			}
			yield break;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002DAC File Offset: 0x00000FAC
		public static void ValidateRulePackageSetId(string rulePackageSetId)
		{
			ADHelpers.GetConfigurationSession(rulePackageSetId);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002DB8 File Offset: 0x00000FB8
		private static IConfigurationSession GetConfigurationSession(string rulePackageSetId)
		{
			Guid guid = Guid.ParseExact(rulePackageSetId, "D");
			IConfigurationSession result;
			try
			{
				if (ADHelpers.OutOfBoxPackageSetId == guid)
				{
					result = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 106, "GetConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Filtering\\src\\platform\\Management\\ADConnector\\impl\\obj\\amd64\\adhelpers.cs");
				}
				else
				{
					result = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.FullyConsistent, guid, 113, "GetConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Filtering\\src\\platform\\Management\\ADConnector\\impl\\obj\\amd64\\adhelpers.cs");
				}
			}
			catch (Exception ex)
			{
				if (Tracing.tracer.Level >= 2 && (Tracing.tracer.Flags & 2048) != 0)
				{
					WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_s(2, 10, TraceProvider.MakeStringArg(rulePackageSetId));
				}
				throw new COMException(ex.Message, -2147220980);
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002E68 File Offset: 0x00001068
		private static TransportRule[] GetRulePackages(IConfigurationSession configSession, string[] rulePackageIds)
		{
			ADObjectId orgContainerId = configSession.GetOrgContainerId();
			ADObjectId childId = orgContainerId.GetChildId("Transport Settings").GetChildId("Rules").GetChildId("ClassificationDefinitions");
			List<QueryFilter> list = new List<QueryFilter>();
			foreach (string propertyValue in rulePackageIds)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, propertyValue));
			}
			QueryFilter filter = new OrFilter(list.ToArray());
			TransportRule[] array = configSession.Find<TransportRule>(childId, QueryScope.SubTree, filter, null, 0);
			if (rulePackageIds.Length != array.Length)
			{
				string text = string.Format("Not all requested rule package sets were found ({0}/{1} found)", array.Length, rulePackageIds.Length);
				if (Tracing.tracer.Level >= 2 && (Tracing.tracer.Flags & 2048) != 0)
				{
					WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_s(2, 11, TraceProvider.MakeStringArg(text));
				}
				throw new COMException(text, -2147220985);
			}
			return array;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002F50 File Offset: 0x00001150
		private static string DecodeClassificationRules(byte[] compressedBytes)
		{
			string result;
			using (Stream stream = new MemoryStream(compressedBytes, false))
			{
				using (Package package = Package.Open(stream, FileMode.Open, FileAccess.Read))
				{
					Uri partUri = PackUriHelper.CreatePartUri(new Uri("config", UriKind.Relative));
					PackagePart part = package.GetPart(partUri);
					Stream stream2 = part.GetStream(FileMode.Open, FileAccess.Read);
					using (TextReader textReader = new StreamReader(stream2, Encoding.Unicode))
					{
						result = textReader.ReadToEnd();
					}
				}
			}
			return result;
		}

		// Token: 0x0400000B RID: 11
		private static readonly Guid OutOfBoxPackageSetId = Guid.Empty;

		// Token: 0x02000006 RID: 6
		internal class ADRulePackageInfo
		{
			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000019 RID: 25 RVA: 0x00003000 File Offset: 0x00001200
			// (set) Token: 0x0600001A RID: 26 RVA: 0x00003008 File Offset: 0x00001208
			public string Xml { get; set; }

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600001B RID: 27 RVA: 0x00003011 File Offset: 0x00001211
			// (set) Token: 0x0600001C RID: 28 RVA: 0x00003019 File Offset: 0x00001219
			public DateTime ModifiedDate { get; set; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600001D RID: 29 RVA: 0x00003022 File Offset: 0x00001222
			// (set) Token: 0x0600001E RID: 30 RVA: 0x0000302A File Offset: 0x0000122A
			public string RulePackageSetId { get; set; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600001F RID: 31 RVA: 0x00003033 File Offset: 0x00001233
			// (set) Token: 0x06000020 RID: 32 RVA: 0x0000303B File Offset: 0x0000123B
			public string RulePackageId { get; set; }
		}
	}
}
