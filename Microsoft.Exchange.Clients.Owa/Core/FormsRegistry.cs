using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200011E RID: 286
	internal sealed class FormsRegistry
	{
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x0004309E File Offset: 0x0004129E
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x000430A6 File Offset: 0x000412A6
		public string InheritsFrom
		{
			get
			{
				return this.inheritsFrom;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x000430AE File Offset: 0x000412AE
		public string BaseExperience
		{
			get
			{
				return this.baseExperience;
			}
		}

		// Token: 0x17000298 RID: 664
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x000430B6 File Offset: 0x000412B6
		public ClientMappingList ClientMappingList
		{
			set
			{
				this.clientMappingList = value;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x000430BF File Offset: 0x000412BF
		public bool IsRichClient
		{
			get
			{
				return this.isRichClient;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x000430C7 File Offset: 0x000412C7
		public bool HasCustomForm
		{
			get
			{
				return this.hasCustomForm;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x000430CF File Offset: 0x000412CF
		public bool IsCustomRegistry
		{
			get
			{
				return this.isCustomRegistry;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00043118 File Offset: 0x00041318
		public IEnumerable<FormKey> CustomizedFormKeys
		{
			get
			{
				List<FormKey> list = new List<FormKey>();
				foreach (FormKey formKey in this.forms.Keys)
				{
					if (this.forms[formKey].IsCustomForm)
					{
						list.Add(formKey);
					}
				}
				return list;
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00043184 File Offset: 0x00041384
		public override int GetHashCode()
		{
			return this.inheritsFrom.GetHashCode() ^ this.name.GetHashCode();
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x000431A0 File Offset: 0x000413A0
		public void Initialize(string name, string baseExperience, string inheritsFrom, string path, bool isRichClient)
		{
			ExTraceGlobals.FormsRegistryCallTracer.TraceDebug((long)this.GetHashCode(), "FormsRegistry.Initialize name = {0}, baseExperience = {1}, inheritsFrom = {2}, path = {3}", new object[]
			{
				name,
				baseExperience,
				inheritsFrom,
				path
			});
			if (inheritsFrom.Length > 0 && baseExperience.Length > 0)
			{
				ExTraceGlobals.FormsRegistryTracer.TraceError<string, string, string>((long)this.GetHashCode(), "A forms registry can not have a base experience and inherit from another registry.  registry = {0}, inheritsFrom = {1}, baseExperience = {2}", name, inheritsFrom, baseExperience);
				throw new OwaInvalidInputException("A forms registry can not have a base experience and inherit from another registry", null, this);
			}
			if (inheritsFrom.Length > 0 && baseExperience.Length > 0)
			{
				ExTraceGlobals.FormsRegistryTracer.TraceError<string>((long)this.GetHashCode(), "A forms registry must have a base experience or inherit from another registry.  registry = {0}", name);
				throw new OwaInvalidInputException("A forms registry must have a base experience or inherit from another registry", null, this);
			}
			this.name = name;
			this.inheritsFrom = inheritsFrom;
			this.baseExperience = baseExperience;
			this.path = path;
			this.isRichClient = isRichClient;
			if (!this.path.EndsWith("/", StringComparison.Ordinal))
			{
				this.path += "/";
			}
			this.isCustomRegistry = (!this.path.EndsWith("Basic/", StringComparison.OrdinalIgnoreCase) && !this.path.EndsWith("Premium/", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x000432C8 File Offset: 0x000414C8
		public void AddForm(FormKey formKey, string form, ulong segmentationFlags)
		{
			if (Utilities.TryParseUri(form) == null)
			{
				form = this.path + form;
			}
			this.AddFormDictionaryEntry(formKey, new FormValue(form, segmentationFlags, this.isCustomRegistry));
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000432FC File Offset: 0x000414FC
		public void AddPreForm(FormKey formKey, string preFormTypeString, ulong segmentationFlags)
		{
			if (formKey == null)
			{
				throw new OwaInvalidInputException("Formkey is empty", null, this);
			}
			if (preFormTypeString == null)
			{
				throw new OwaInvalidInputException("preFormTypeString is empty", null, this);
			}
			ExTraceGlobals.FormsRegistryCallTracer.TraceDebug<string>((long)this.GetHashCode(), "FormsRegistry.AddPreForm - looking up type - typestring = ({0})", preFormTypeString);
			Type type = null;
			try
			{
				type = Type.GetType(preFormTypeString, true);
			}
			catch (TypeLoadException)
			{
				throw new OwaInvalidInputException("A forms registry preform type must be inherited from IPreformAction Interface - failed loading Class " + preFormTypeString, null, this);
			}
			catch (TargetInvocationException)
			{
				throw new OwaInvalidInputException("A forms registry preform type must be inherited from IPreformAction Interface - failed loading Class " + preFormTypeString, null, this);
			}
			if (type.IsClass && typeof(IPreFormAction).IsAssignableFrom(type))
			{
				this.AddFormDictionaryEntry(formKey, new FormValue(type, segmentationFlags, this.isCustomRegistry));
				ExTraceGlobals.FormsRegistryDataTracer.TraceDebug<string>((long)this.GetHashCode(), "FormsRegistry.AddPreForm - added type - type = ({0})", type.FullName);
				return;
			}
			string message = "A forms registry preform type must be inherited from IPreformAction Interface - failed loading Class " + preFormTypeString;
			ExTraceGlobals.FormsRegistryTracer.TraceDebug<string>((long)this.GetHashCode(), "FormsRegistry.AddPreForm - failed to add type - type = ({0})", type.FullName);
			throw new OwaInvalidInputException(message, null, this);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00043410 File Offset: 0x00041610
		public FormValue LookupForm(FormKey formKey)
		{
			ExTraceGlobals.FormsRegistryCallTracer.TraceDebug<string>((long)this.GetHashCode(), "FormsRegistry.LookupForm in registry {0}", this.name);
			if (!this.forms.ContainsKey(formKey))
			{
				ExTraceGlobals.FormsRegistryDataTracer.TraceDebug<FormKey>((long)this.GetHashCode(), "FormsRegistry.LookupForm - no form found - key = ({0})", formKey);
				return null;
			}
			FormValue formValue = this.forms[formKey];
			ExTraceGlobals.FormsRegistryDataTracer.TraceDebug<object, FormKey>((long)this.GetHashCode(), "FormsRegistry.LookupForm - found form - form = {0}, key = ({1})", formValue.Value, formKey);
			return formValue;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0004348C File Offset: 0x0004168C
		public Experience[] LookupExperiences(string application, UserAgentParser.UserAgentVersion version, string platform, ClientControl control)
		{
			ExTraceGlobals.FormsRegistryCallTracer.TraceDebug((long)this.GetHashCode(), "FormsRegistry.LookupExperiences application = {0}, version = {1}, platform = {2}, control = {3}", new object[]
			{
				application,
				version,
				platform,
				control
			});
			Hashtable hashtable = new Hashtable(1);
			ArrayList arrayList = new ArrayList(1);
			FormsRegistry.LookupExperienceState lookupExperienceState = FormsRegistry.LookupExperienceState.ExactMatch;
			while (FormsRegistry.LookupExperienceState.Done != lookupExperienceState)
			{
				switch (lookupExperienceState)
				{
				case FormsRegistry.LookupExperienceState.ExactMatch:
					lookupExperienceState = FormsRegistry.LookupExperienceState.Control;
					break;
				case FormsRegistry.LookupExperienceState.Control:
					lookupExperienceState = FormsRegistry.LookupExperienceState.Platform;
					if (control == ClientControl.None)
					{
						continue;
					}
					control = ClientControl.None;
					break;
				case FormsRegistry.LookupExperienceState.Platform:
					lookupExperienceState = FormsRegistry.LookupExperienceState.Application;
					platform = string.Empty;
					break;
				case FormsRegistry.LookupExperienceState.Application:
					lookupExperienceState = FormsRegistry.LookupExperienceState.Done;
					application = string.Empty;
					break;
				}
				int i;
				int num;
				if (this.clientMappingList.FindMatchingRange(application, platform, control, version, out i, out num))
				{
					while (i <= num)
					{
						ClientMapping clientMapping = this.clientMappingList[num];
						Experience experience = clientMapping.Experience;
						if (!hashtable.ContainsKey(experience))
						{
							ExTraceGlobals.FormsRegistryDataTracer.TraceDebug<string, ClientMapping>((long)this.GetHashCode(), "Matched experience. name = {0}, client mapping = ({1})", experience.Name, clientMapping);
							hashtable.Add(experience, string.Empty);
							arrayList.Add(experience);
						}
						num--;
					}
				}
			}
			ExTraceGlobals.FormsRegistryTracer.TraceDebug<int>((long)this.GetHashCode(), "FormsRegistry.LookupExperiences - Exit.  Matched {0} Experiences", arrayList.Count);
			return (Experience[])arrayList.ToArray(typeof(Experience));
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x000435E1 File Offset: 0x000417E1
		private void AddFormDictionaryEntry(FormKey formKey, FormValue formValue)
		{
			if (this.forms.ContainsKey(formKey, FormDictionary.MatchMode.ExactMatch))
			{
				throw new OwaInvalidInputException("Duplicate form registry key exists", null, this);
			}
			this.forms.Add(formKey, formValue);
			this.hasCustomForm = formValue.IsCustomForm;
		}

		// Token: 0x040006F2 RID: 1778
		public static FastEnumParser ClientControlParser = new FastEnumParser(typeof(ClientControl));

		// Token: 0x040006F3 RID: 1779
		public static FastEnumParser ApplicationElementParser = new FastEnumParser(typeof(ApplicationElement));

		// Token: 0x040006F4 RID: 1780
		private string name = string.Empty;

		// Token: 0x040006F5 RID: 1781
		private string path = string.Empty;

		// Token: 0x040006F6 RID: 1782
		private string inheritsFrom = string.Empty;

		// Token: 0x040006F7 RID: 1783
		private string baseExperience = string.Empty;

		// Token: 0x040006F8 RID: 1784
		private FormDictionary forms = new FormDictionary();

		// Token: 0x040006F9 RID: 1785
		private ClientMappingList clientMappingList;

		// Token: 0x040006FA RID: 1786
		private bool isRichClient;

		// Token: 0x040006FB RID: 1787
		private bool hasCustomForm;

		// Token: 0x040006FC RID: 1788
		private bool isCustomRegistry;

		// Token: 0x0200011F RID: 287
		private enum LookupExperienceState
		{
			// Token: 0x040006FE RID: 1790
			ExactMatch,
			// Token: 0x040006FF RID: 1791
			Control,
			// Token: 0x04000700 RID: 1792
			Platform,
			// Token: 0x04000701 RID: 1793
			Application,
			// Token: 0x04000702 RID: 1794
			Done
		}
	}
}
