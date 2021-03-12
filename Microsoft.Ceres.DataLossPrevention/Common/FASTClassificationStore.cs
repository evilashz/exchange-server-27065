using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.Ceres.Diagnostics;
using Microsoft.Ceres.Flighting;
using Microsoft.Office.CompliancePolicy.Classification;

namespace Microsoft.Ceres.DataLossPrevention.Common
{
	// Token: 0x02000007 RID: 7
	internal class FASTClassificationStore : IClassificationRuleStore, IRulePackageLoader
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002C40 File Offset: 0x00000E40
		internal FASTClassificationStore()
		{
			this.ruleSets = new Dictionary<string, ClassificationRuleSet>();
			this.LoadDefaultRuleSet();
			this.defaultRuleDetails = new FASTClassificationStore.CachedDetails(this.defaultRuleSet.PackageId, FASTClassificationStore.DetailsCacheExpiration, new FASTClassificationStore.CachedDetails.LoadDetailsDelegate(this.LoadRules));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002C80 File Offset: 0x00000E80
		private void LoadDefaultRuleSet()
		{
			ULS.SendTraceTag(4850011U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "FASTClassificationStore.LoadDefaultRuleSet :: loading default classification rules");
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Stream stream = null;
			try
			{
				stream = executingAssembly.GetManifestResourceStream("defaultClassificationRules");
				using (TextReader textReader = new StreamReader(stream))
				{
					stream = null;
					this.defaultRuleSet = new ClassificationRuleSet(textReader);
				}
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
				}
			}
			this.ruleSets.Add(this.defaultRuleSet.PackageId, this.defaultRuleSet);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002D1C File Offset: 0x00000F1C
		public RULE_PACKAGE_DETAILS[] GetRulePackageDetails(IClassificationItem classificationItem)
		{
			if (classificationItem == null)
			{
				return null;
			}
			ULS.SendTraceTag(4850012U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "FASTClassificationStore.GetRulePackageDetails :: ClassificationItem :: {0}", new object[]
			{
				classificationItem.ItemId
			});
			RULE_PACKAGE_DETAILS value = this.defaultRuleDetails.Value;
			if (value.RuleIDs.Length > 0)
			{
				return new RULE_PACKAGE_DETAILS[]
				{
					value
				};
			}
			return null;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002D84 File Offset: 0x00000F84
		public RuleDefinitionDetails GetRuleDetails(string ruleId, string localeName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002D8B File Offset: 0x00000F8B
		public IEnumerable<RuleDefinitionDetails> GetAllRuleDetails(bool loadLocalizableData = false)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002D92 File Offset: 0x00000F92
		internal long? GetResultBase(string packageId, string ruleId)
		{
			return this.ruleSets[packageId].RuleIdToCode(ruleId);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002DA6 File Offset: 0x00000FA6
		internal string[] AllRuleIds()
		{
			return this.defaultRuleSet.AllRuleIds;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002DB3 File Offset: 0x00000FB3
		internal long? RuleIdToPrefixCode(string ruleId)
		{
			return this.defaultRuleSet.RuleIdToCode(ruleId);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002DC1 File Offset: 0x00000FC1
		internal string RuleIdToRuleName(string ruleId)
		{
			return this.defaultRuleSet.RuleIdToRuleName(ruleId);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002DCF File Offset: 0x00000FCF
		internal string RuleNameToRuleId(string ruleName)
		{
			return this.defaultRuleSet.RuleNameToRuleId(ruleName);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public void GetRulePackages(uint ulRulePackageRequestDetailsSize, RULE_PACKAGE_REQUEST_DETAILS[] rulePackageRequestDetails)
		{
			if (rulePackageRequestDetails == null)
			{
				return;
			}
			int num = 0;
			while ((long)num < (long)((ulong)ulRulePackageRequestDetailsSize))
			{
				ULS.SendTraceTag(4850013U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "FASTClassificationStore.GetRulePackages :: Requested Package :: {0}", new object[]
				{
					rulePackageRequestDetails[num].RulePackageID
				});
				ClassificationRuleSet classificationRuleSet = this.ruleSets[rulePackageRequestDetails[num].RulePackageID];
				rulePackageRequestDetails[num].RulePackage = classificationRuleSet.RuleXML;
				num++;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002E56 File Offset: 0x00001056
		public void GetUpdatedRulePackageInfo(uint ulRulePackageTimestampDetailsSize, RULE_PACKAGE_TIMESTAMP_DETAILS[] rulePackageTimestampDetails)
		{
			ULS.SendTraceTag(4850014U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "FASTClassificationStore.GetUpdatedRulePackageInfo");
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002E70 File Offset: 0x00001070
		public IList<string> Scopes()
		{
			IList<string> list = new List<string>(this.defaultRuleSet.AllRuleIds.Length * 2 + 1);
			list.Add("ClassificationType");
			foreach (string ruleId in this.defaultRuleSet.AllRuleIds)
			{
				string str = this.defaultRuleSet.RuleIdToQueryIdentifier(ruleId);
				list.Add("ClassificationCount" + str);
				list.Add("ClassificationConfidence" + str);
			}
			return list;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002EF4 File Offset: 0x000010F4
		public IList<string> SecurityScopes()
		{
			IList<string> list = this.Scopes();
			list.Add("ClassificationLastScan");
			list.Add("ClassificationCount");
			list.Add("ClassificationConfidence");
			list.Add("SensitiveType");
			list.Add("SensitiveMatchConfidence");
			list.Add("SensitiveMatchCount");
			return list;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F4C File Offset: 0x0000114C
		private RULE_PACKAGE_DETAILS LoadRules(string packageId)
		{
			ULS.SendTraceTag(4850015U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 50, "FASTClassificationStore.GetDefaultRulePackageDetails :: Loading rules for package [{0}]", new object[]
			{
				packageId
			});
			RULE_PACKAGE_DETAILS result = default(RULE_PACKAGE_DETAILS);
			result.RulePackageID = packageId;
			result.RulePackageSetID = "NA";
			List<string> list = new List<string>();
			if (this.defaultRuleSet.PackageId == packageId)
			{
				foreach (string text in this.defaultRuleSet.AllRuleIds)
				{
					if (VariantConfiguration.IsFeatureEnabled(40, text))
					{
						ULS.SendTraceTag(4850016U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "FASTClassificationStore.GetDefaultRulePackageDetails :: Adding rule [{0}]", new object[]
						{
							text
						});
						list.Add(text);
					}
				}
			}
			result.RuleIDs = list.ToArray();
			return result;
		}

		// Token: 0x04000026 RID: 38
		internal const string CountPrefix = "ClassificationCount";

		// Token: 0x04000027 RID: 39
		internal const string ConfidencePrefix = "ClassificationConfidence";

		// Token: 0x04000028 RID: 40
		internal static readonly TimeSpan DetailsCacheExpiration = new TimeSpan(0, 5, 0);

		// Token: 0x04000029 RID: 41
		private Dictionary<string, ClassificationRuleSet> ruleSets;

		// Token: 0x0400002A RID: 42
		private ClassificationRuleSet defaultRuleSet;

		// Token: 0x0400002B RID: 43
		private FASTClassificationStore.CachedDetails defaultRuleDetails;

		// Token: 0x02000008 RID: 8
		private class CachedDetails
		{
			// Token: 0x06000042 RID: 66 RVA: 0x0000302C File Offset: 0x0000122C
			internal CachedDetails(string packageId, TimeSpan expirationTime, FASTClassificationStore.CachedDetails.LoadDetailsDelegate loadDelegate)
			{
				this.packageId = packageId;
				this.expirationTime = expirationTime;
				this.loadDelegate = loadDelegate;
				this.cachedDetails = this.loadDelegate(this.packageId);
				this.cacheExpiration = DateTime.UtcNow + this.expirationTime;
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000043 RID: 67 RVA: 0x000030A0 File Offset: 0x000012A0
			internal RULE_PACKAGE_DETAILS Value
			{
				get
				{
					DateTime utcNow = DateTime.UtcNow;
					if (this.cacheExpiration < utcNow && this.loadingThread == 0)
					{
						lock (this.calculatingLock)
						{
							if (this.loadingThread == 0)
							{
								this.loadingThread = Thread.CurrentThread.ManagedThreadId;
							}
						}
						if (this.loadingThread == Thread.CurrentThread.ManagedThreadId)
						{
							this.cacheExpiration = utcNow + FASTClassificationStore.DetailsCacheExpiration;
							bool flag2 = false;
							RULE_PACKAGE_DETAILS rule_PACKAGE_DETAILS = default(RULE_PACKAGE_DETAILS);
							try
							{
								rule_PACKAGE_DETAILS = this.loadDelegate(this.packageId);
								flag2 = true;
							}
							catch (ThreadAbortException)
							{
							}
							catch (OutOfMemoryException)
							{
								throw;
							}
							catch (Exception ex)
							{
								ULSEvent.eventSearch_DataLossPreventionRuleLoadFailed.Send(ex.ToString());
								ULS.SendTraceTag(5018003U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 10, "Loading rules in package: {0} failed with exception: {1}", new object[]
								{
									this.packageId,
									ex
								});
							}
							if (flag2)
							{
								this.detailsRWL.AcquireWriterLock(this.ReaderWriterLockTimeout);
								try
								{
									this.cachedDetails = rule_PACKAGE_DETAILS;
								}
								finally
								{
									this.loadingThread = 0;
									this.detailsRWL.ReleaseWriterLock();
								}
							}
						}
					}
					this.detailsRWL.AcquireReaderLock(this.ReaderWriterLockTimeout);
					RULE_PACKAGE_DETAILS result;
					try
					{
						result = this.cachedDetails;
					}
					finally
					{
						this.detailsRWL.ReleaseReaderLock();
					}
					return result;
				}
			}

			// Token: 0x0400002C RID: 44
			private int ReaderWriterLockTimeout = 10;

			// Token: 0x0400002D RID: 45
			private object calculatingLock = new object();

			// Token: 0x0400002E RID: 46
			private ReaderWriterLock detailsRWL = new ReaderWriterLock();

			// Token: 0x0400002F RID: 47
			private int loadingThread;

			// Token: 0x04000030 RID: 48
			private string packageId;

			// Token: 0x04000031 RID: 49
			private TimeSpan expirationTime;

			// Token: 0x04000032 RID: 50
			private FASTClassificationStore.CachedDetails.LoadDetailsDelegate loadDelegate;

			// Token: 0x04000033 RID: 51
			private DateTime cacheExpiration;

			// Token: 0x04000034 RID: 52
			private RULE_PACKAGE_DETAILS cachedDetails;

			// Token: 0x02000009 RID: 9
			// (Invoke) Token: 0x06000045 RID: 69
			internal delegate RULE_PACKAGE_DETAILS LoadDetailsDelegate(string packageId);
		}
	}
}
