using System;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000122 RID: 290
	internal sealed class FormsRegistryManager
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00043CA1 File Offset: 0x00041EA1
		public static bool HasCustomForm
		{
			get
			{
				return FormsRegistryManager.hasCustomForm;
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00043CA8 File Offset: 0x00041EA8
		private FormsRegistryManager()
		{
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00043CB0 File Offset: 0x00041EB0
		public static void Initialize(string directory)
		{
			ExTraceGlobals.FormsRegistryCallTracer.TraceDebug<string>(0L, "FormsRegistryManager.Initialize directory = {0}", directory);
			FormsRegistryLoader formsRegistryLoader = new FormsRegistryLoader();
			try
			{
				formsRegistryLoader.LoadRegistries(directory + "forms/");
			}
			catch (OwaInvalidInputException innerException)
			{
				throw new OwaFormsRegistryInitializationException("Unable to initialize forms registries", innerException);
			}
			FormsRegistryManager.baseExperienceClientMappingList = formsRegistryLoader.BaseClientMappings;
			FormsRegistryManager.isLoaded = true;
			FormsRegistryManager.hasCustomForm = formsRegistryLoader.HasCustomForm;
			ExTraceGlobals.FormsRegistryTracer.TraceDebug(0L, "FormsRegistryManager initialized succesfully.");
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00043D34 File Offset: 0x00041F34
		public static bool IsLoaded
		{
			get
			{
				return FormsRegistryManager.isLoaded;
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00043D3C File Offset: 0x00041F3C
		public static Experience[] LookupExperiences(string application, UserAgentParser.UserAgentVersion version, string platform, ClientControl control, bool isRichClientFeatureOn)
		{
			ExTraceGlobals.FormsRegistryCallTracer.TraceDebug(0L, "FormsRegistryManager.LookupExperiences application = {0}, version = {1}, platform = {2}, control = {3}", new object[]
			{
				application,
				version,
				platform,
				control
			});
			int num;
			int num2;
			if (!FormsRegistryManager.baseExperienceClientMappingList.FindMatchingRange(application, platform, control, version, out num, out num2))
			{
				return null;
			}
			FormsRegistry formsRegistry = null;
			if (isRichClientFeatureOn)
			{
				formsRegistry = FormsRegistryManager.baseExperienceClientMappingList[num2].Experience.FormsRegistry;
			}
			else
			{
				for (int i = num2; i >= num; i--)
				{
					formsRegistry = FormsRegistryManager.baseExperienceClientMappingList[i].Experience.FormsRegistry;
					if (!formsRegistry.IsRichClient)
					{
						break;
					}
					formsRegistry = null;
				}
				if (formsRegistry == null)
				{
					return null;
				}
			}
			ExTraceGlobals.FormsRegistryDataTracer.TraceDebug<string>(0L, "Matched registry = {0}", formsRegistry.Name);
			return formsRegistry.LookupExperiences(application, version, platform, control);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00043E08 File Offset: 0x00042008
		public static FormValue LookupForm(FormKey formKey, Experience[] experiences)
		{
			ExTraceGlobals.FormsRegistryCallTracer.TraceDebug<FormKey>(0L, "FormsRegistryManager.LookupForm key = {0}", formKey);
			if (experiences == null)
			{
				throw new ArgumentNullException("experiences", "There must be at least one experience provided");
			}
			if (experiences.Length == 0)
			{
				throw new ArgumentOutOfRangeException("experiences", "There must be at least one experience provided");
			}
			FormValue formValue = FormsRegistryManager.LookupFormInExperiences(formKey, experiences);
			if (formValue == null)
			{
				ExTraceGlobals.FormsRegistryTracer.TraceDebug(0L, "Downgrading state to wildcard");
				formKey.State = string.Empty;
				formValue = FormsRegistryManager.LookupFormInExperiences(formKey, experiences);
				if (formValue == null)
				{
					ExTraceGlobals.FormsRegistryTracer.TraceDebug(0L, "Downgrading action to wildcard");
					string action = formKey.Action;
					formKey.Action = string.Empty;
					formValue = FormsRegistryManager.LookupFormInExperiences(formKey, experiences);
					if (formValue == null)
					{
						ExTraceGlobals.FormsRegistryTracer.TraceDebug(0L, "Downgrading class to wildcard");
						formKey.Class = string.Empty;
						formKey.Action = action;
						ExTraceGlobals.FormsRegistryTracer.TraceDebug(0L, "Restoring action");
						formValue = FormsRegistryManager.LookupFormInExperiences(formKey, experiences);
						if (formValue == null)
						{
							formKey.Action = string.Empty;
							ExTraceGlobals.FormsRegistryTracer.TraceDebug(0L, "Downgrading action to wildcard");
							formValue = FormsRegistryManager.LookupFormInExperiences(formKey, experiences);
						}
					}
				}
			}
			return formValue;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00043F1C File Offset: 0x0004211C
		private static FormValue LookupFormInExperiences(FormKey formKey, Experience[] experiences)
		{
			ExTraceGlobals.FormsRegistryCallTracer.TraceDebug<FormKey>(0L, "FormsRegistryManager.LookupFormInExperiences key = {0}", formKey);
			FormValue formValue = null;
			int num = experiences.Length;
			for (int i = 0; i < num; i++)
			{
				Experience experience = experiences[i];
				formKey.Experience = experience.Name;
				formValue = experience.FormsRegistry.LookupForm(formKey);
				if (formValue != null)
				{
					break;
				}
			}
			return formValue;
		}

		// Token: 0x0400070B RID: 1803
		private const string RegistryFolder = "forms/";

		// Token: 0x0400070C RID: 1804
		private static ClientMappingList baseExperienceClientMappingList;

		// Token: 0x0400070D RID: 1805
		private static bool isLoaded;

		// Token: 0x0400070E RID: 1806
		private static bool hasCustomForm;
	}
}
