using System;
using System.IO;
using System.Runtime.Hosting;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006E6 RID: 1766
	[SecuritySafeCritical]
	[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal static class CmsUtils
	{
		// Token: 0x06004FF3 RID: 20467 RVA: 0x00119454 File Offset: 0x00117654
		internal static void GetEntryPoint(ActivationContext activationContext, out string fileName, out string parameters)
		{
			parameters = null;
			fileName = null;
			ICMS applicationComponentManifest = activationContext.ApplicationComponentManifest;
			if (applicationComponentManifest == null || applicationComponentManifest.EntryPointSection == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NoMain"));
			}
			IEnumUnknown enumUnknown = (IEnumUnknown)applicationComponentManifest.EntryPointSection._NewEnum;
			uint num = 0U;
			object[] array = new object[1];
			if (enumUnknown.Next(1U, array, ref num) == 0 && num == 1U)
			{
				IEntryPointEntry entryPointEntry = (IEntryPointEntry)array[0];
				EntryPointEntry allData = entryPointEntry.AllData;
				if (allData.CommandLine_File != null && allData.CommandLine_File.Length > 0)
				{
					fileName = allData.CommandLine_File;
				}
				else
				{
					object obj = null;
					if (allData.Identity != null)
					{
						((ISectionWithReferenceIdentityKey)applicationComponentManifest.AssemblyReferenceSection).Lookup(allData.Identity, out obj);
						IAssemblyReferenceEntry assemblyReferenceEntry = (IAssemblyReferenceEntry)obj;
						fileName = assemblyReferenceEntry.DependentAssembly.Codebase;
					}
				}
				parameters = allData.CommandLine_Parameters;
			}
		}

		// Token: 0x06004FF4 RID: 20468 RVA: 0x00119534 File Offset: 0x00117734
		internal static IAssemblyReferenceEntry[] GetDependentAssemblies(ActivationContext activationContext)
		{
			IAssemblyReferenceEntry[] array = null;
			ICMS applicationComponentManifest = activationContext.ApplicationComponentManifest;
			if (applicationComponentManifest == null)
			{
				return null;
			}
			ISection assemblyReferenceSection = applicationComponentManifest.AssemblyReferenceSection;
			uint num = (assemblyReferenceSection != null) ? assemblyReferenceSection.Count : 0U;
			if (num > 0U)
			{
				uint num2 = 0U;
				array = new IAssemblyReferenceEntry[num];
				IEnumUnknown enumUnknown = (IEnumUnknown)assemblyReferenceSection._NewEnum;
				int num3 = enumUnknown.Next(num, array, ref num2);
				if (num2 != num || num3 < 0)
				{
					return null;
				}
			}
			return array;
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x00119598 File Offset: 0x00117798
		internal static string GetEntryPointFullPath(ActivationArguments activationArguments)
		{
			return CmsUtils.GetEntryPointFullPath(activationArguments.ActivationContext);
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x001195A8 File Offset: 0x001177A8
		internal static string GetEntryPointFullPath(ActivationContext activationContext)
		{
			string text;
			string text2;
			CmsUtils.GetEntryPoint(activationContext, out text, out text2);
			if (!string.IsNullOrEmpty(text))
			{
				string text3 = activationContext.ApplicationDirectory;
				if (text3 == null || text3.Length == 0)
				{
					text3 = Directory.UnsafeGetCurrentDirectory();
				}
				text = Path.Combine(text3, text);
			}
			return text;
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x001195E8 File Offset: 0x001177E8
		internal static bool CompareIdentities(ActivationContext activationContext1, ActivationContext activationContext2)
		{
			if (activationContext1 == null || activationContext2 == null)
			{
				return activationContext1 == activationContext2;
			}
			return IsolationInterop.AppIdAuthority.AreDefinitionsEqual(0U, activationContext1.Identity.Identity, activationContext2.Identity.Identity);
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x00119618 File Offset: 0x00117818
		internal static bool CompareIdentities(ApplicationIdentity applicationIdentity1, ApplicationIdentity applicationIdentity2, ApplicationVersionMatch versionMatch)
		{
			if (applicationIdentity1 == null || applicationIdentity2 == null)
			{
				return applicationIdentity1 == applicationIdentity2;
			}
			uint flags;
			if (versionMatch != ApplicationVersionMatch.MatchExactVersion)
			{
				if (versionMatch != ApplicationVersionMatch.MatchAllVersions)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
					{
						(int)versionMatch
					}), "versionMatch");
				}
				flags = 1U;
			}
			else
			{
				flags = 0U;
			}
			return IsolationInterop.AppIdAuthority.AreDefinitionsEqual(flags, applicationIdentity1.Identity, applicationIdentity2.Identity);
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x0011967C File Offset: 0x0011787C
		internal static string GetFriendlyName(ActivationContext activationContext)
		{
			ICMS deploymentComponentManifest = activationContext.DeploymentComponentManifest;
			IMetadataSectionEntry metadataSectionEntry = (IMetadataSectionEntry)deploymentComponentManifest.MetadataSectionEntry;
			IDescriptionMetadataEntry descriptionData = metadataSectionEntry.DescriptionData;
			string result = string.Empty;
			if (descriptionData != null)
			{
				DescriptionMetadataEntry allData = descriptionData.AllData;
				result = ((allData.Publisher != null) ? string.Format("{0} {1}", allData.Publisher, allData.Product) : allData.Product);
			}
			return result;
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x001196E0 File Offset: 0x001178E0
		internal static void CreateActivationContext(string fullName, string[] manifestPaths, bool useFusionActivationContext, out ApplicationIdentity applicationIdentity, out ActivationContext activationContext)
		{
			applicationIdentity = new ApplicationIdentity(fullName);
			activationContext = null;
			if (useFusionActivationContext)
			{
				if (manifestPaths != null)
				{
					activationContext = new ActivationContext(applicationIdentity, manifestPaths);
					return;
				}
				activationContext = new ActivationContext(applicationIdentity);
			}
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x0011970A File Offset: 0x0011790A
		internal static Evidence MergeApplicationEvidence(Evidence evidence, ApplicationIdentity applicationIdentity, ActivationContext activationContext, string[] activationData)
		{
			return CmsUtils.MergeApplicationEvidence(evidence, applicationIdentity, activationContext, activationData, null);
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x00119718 File Offset: 0x00117918
		internal static Evidence MergeApplicationEvidence(Evidence evidence, ApplicationIdentity applicationIdentity, ActivationContext activationContext, string[] activationData, ApplicationTrust applicationTrust)
		{
			Evidence evidence2 = new Evidence();
			ActivationArguments evidence3 = (activationContext == null) ? new ActivationArguments(applicationIdentity, activationData) : new ActivationArguments(activationContext, activationData);
			evidence2 = new Evidence();
			evidence2.AddHostEvidence<ActivationArguments>(evidence3);
			if (applicationTrust != null)
			{
				evidence2.AddHostEvidence<ApplicationTrust>(applicationTrust);
			}
			if (activationContext != null)
			{
				Evidence applicationEvidence = new ApplicationSecurityInfo(activationContext).ApplicationEvidence;
				if (applicationEvidence != null)
				{
					evidence2.MergeWithNoDuplicates(applicationEvidence);
				}
			}
			if (evidence != null)
			{
				evidence2.MergeWithNoDuplicates(evidence);
			}
			return evidence2;
		}
	}
}
