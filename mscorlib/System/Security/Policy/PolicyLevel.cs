using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;
using System.Threading;

namespace System.Security.Policy
{
	// Token: 0x02000338 RID: 824
	[ComVisible(true)]
	[Serializable]
	public sealed class PolicyLevel
	{
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060029B7 RID: 10679 RVA: 0x00099F74 File Offset: 0x00098174
		private static object InternalSyncObject
		{
			get
			{
				if (PolicyLevel.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref PolicyLevel.s_InternalSyncObject, value, null);
				}
				return PolicyLevel.s_InternalSyncObject;
			}
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x00099FA0 File Offset: 0x000981A0
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_label != null)
			{
				this.DeriveTypeFromLabel();
			}
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x00099FB0 File Offset: 0x000981B0
		private void DeriveTypeFromLabel()
		{
			if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_User")))
			{
				this.m_type = PolicyLevelType.User;
				return;
			}
			if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_Machine")))
			{
				this.m_type = PolicyLevelType.Machine;
				return;
			}
			if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_Enterprise")))
			{
				this.m_type = PolicyLevelType.Enterprise;
				return;
			}
			if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_AppDomain")))
			{
				this.m_type = PolicyLevelType.AppDomain;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Policy_Default"));
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x0009A048 File Offset: 0x00098248
		private string DeriveLabelFromType()
		{
			switch (this.m_type)
			{
			case PolicyLevelType.User:
				return Environment.GetResourceString("Policy_PL_User");
			case PolicyLevelType.Machine:
				return Environment.GetResourceString("Policy_PL_Machine");
			case PolicyLevelType.Enterprise:
				return Environment.GetResourceString("Policy_PL_Enterprise");
			case PolicyLevelType.AppDomain:
				return Environment.GetResourceString("Policy_PL_AppDomain");
			default:
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)this.m_type
				}));
			}
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x0009A0C3 File Offset: 0x000982C3
		private PolicyLevel()
		{
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x0009A0CB File Offset: 0x000982CB
		[SecurityCritical]
		internal PolicyLevel(PolicyLevelType type) : this(type, PolicyLevel.GetLocationFromType(type))
		{
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x0009A0DA File Offset: 0x000982DA
		internal PolicyLevel(PolicyLevelType type, string path) : this(type, path, ConfigId.None)
		{
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x0009A0E8 File Offset: 0x000982E8
		internal PolicyLevel(PolicyLevelType type, string path, ConfigId configId)
		{
			this.m_type = type;
			this.m_path = path;
			this.m_loaded = (path == null);
			if (this.m_path == null)
			{
				this.m_rootCodeGroup = this.CreateDefaultAllGroup();
				this.SetFactoryPermissionSets();
				this.SetDefaultFullTrustAssemblies();
			}
			this.m_configId = configId;
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x0009A13C File Offset: 0x0009833C
		[SecurityCritical]
		internal static string GetLocationFromType(PolicyLevelType type)
		{
			switch (type)
			{
			case PolicyLevelType.User:
				return Config.UserDirectory + "security.config";
			case PolicyLevelType.Machine:
				return Config.MachineDirectory + "security.config";
			case PolicyLevelType.Enterprise:
				return Config.MachineDirectory + "enterprisesec.config";
			default:
				return null;
			}
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x0009A18E File Offset: 0x0009838E
		[SecuritySafeCritical]
		[Obsolete("AppDomain policy levels are obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public static PolicyLevel CreateAppDomainLevel()
		{
			return new PolicyLevel(PolicyLevelType.AppDomain);
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060029C1 RID: 10689 RVA: 0x0009A196 File Offset: 0x00098396
		public string Label
		{
			get
			{
				if (this.m_label == null)
				{
					this.m_label = this.DeriveLabelFromType();
				}
				return this.m_label;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060029C2 RID: 10690 RVA: 0x0009A1B2 File Offset: 0x000983B2
		[ComVisible(false)]
		public PolicyLevelType Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060029C3 RID: 10691 RVA: 0x0009A1BA File Offset: 0x000983BA
		internal ConfigId ConfigId
		{
			get
			{
				return this.m_configId;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060029C4 RID: 10692 RVA: 0x0009A1C2 File Offset: 0x000983C2
		internal string Path
		{
			get
			{
				return this.m_path;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060029C5 RID: 10693 RVA: 0x0009A1CA File Offset: 0x000983CA
		public string StoreLocation
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
			get
			{
				return PolicyLevel.GetLocationFromType(this.m_type);
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x0009A1D7 File Offset: 0x000983D7
		// (set) Token: 0x060029C7 RID: 10695 RVA: 0x0009A1E5 File Offset: 0x000983E5
		public CodeGroup RootCodeGroup
		{
			[SecuritySafeCritical]
			get
			{
				this.CheckLoaded();
				return this.m_rootCodeGroup;
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("RootCodeGroup");
				}
				this.CheckLoaded();
				this.m_rootCodeGroup = value.Copy();
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x0009A208 File Offset: 0x00098408
		public IList NamedPermissionSets
		{
			[SecuritySafeCritical]
			get
			{
				this.CheckLoaded();
				this.LoadAllPermissionSets();
				ArrayList arrayList = new ArrayList(this.m_namedPermissionSets.Count);
				foreach (object obj in this.m_namedPermissionSets)
				{
					arrayList.Add(((NamedPermissionSet)obj).Copy());
				}
				return arrayList;
			}
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x0009A260 File Offset: 0x00098460
		public CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			return this.RootCodeGroup.ResolveMatchingCodeGroups(evidence);
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x0009A27C File Offset: 0x0009847C
		[Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
		public void AddFullTrustAssembly(StrongName sn)
		{
			if (sn == null)
			{
				throw new ArgumentNullException("sn");
			}
			this.AddFullTrustAssembly(new StrongNameMembershipCondition(sn.PublicKey, sn.Name, sn.Version));
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x0009A2AC File Offset: 0x000984AC
		[SecuritySafeCritical]
		[Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
		public void AddFullTrustAssembly(StrongNameMembershipCondition snMC)
		{
			if (snMC == null)
			{
				throw new ArgumentNullException("snMC");
			}
			this.CheckLoaded();
			IEnumerator enumerator = this.m_fullTrustAssemblies.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (((StrongNameMembershipCondition)enumerator.Current).Equals(snMC))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_AssemblyAlreadyFullTrust"));
				}
			}
			ArrayList fullTrustAssemblies = this.m_fullTrustAssemblies;
			lock (fullTrustAssemblies)
			{
				this.m_fullTrustAssemblies.Add(snMC);
			}
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x0009A340 File Offset: 0x00098540
		[Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
		public void RemoveFullTrustAssembly(StrongName sn)
		{
			if (sn == null)
			{
				throw new ArgumentNullException("assembly");
			}
			this.RemoveFullTrustAssembly(new StrongNameMembershipCondition(sn.PublicKey, sn.Name, sn.Version));
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x0009A370 File Offset: 0x00098570
		[SecuritySafeCritical]
		[Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
		public void RemoveFullTrustAssembly(StrongNameMembershipCondition snMC)
		{
			if (snMC == null)
			{
				throw new ArgumentNullException("snMC");
			}
			this.CheckLoaded();
			object obj = null;
			IEnumerator enumerator = this.m_fullTrustAssemblies.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (((StrongNameMembershipCondition)enumerator.Current).Equals(snMC))
				{
					obj = enumerator.Current;
					break;
				}
			}
			if (obj == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_AssemblyNotFullTrust"));
			}
			ArrayList fullTrustAssemblies = this.m_fullTrustAssemblies;
			lock (fullTrustAssemblies)
			{
				this.m_fullTrustAssemblies.Remove(obj);
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x0009A414 File Offset: 0x00098614
		[Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
		public IList FullTrustAssemblies
		{
			[SecuritySafeCritical]
			get
			{
				this.CheckLoaded();
				return new ArrayList(this.m_fullTrustAssemblies);
			}
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x0009A428 File Offset: 0x00098628
		[SecuritySafeCritical]
		public void AddNamedPermissionSet(NamedPermissionSet permSet)
		{
			if (permSet == null)
			{
				throw new ArgumentNullException("permSet");
			}
			this.CheckLoaded();
			this.LoadAllPermissionSets();
			lock (this)
			{
				IEnumerator enumerator = this.m_namedPermissionSets.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (((NamedPermissionSet)enumerator.Current).Name.Equals(permSet.Name))
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_DuplicateName"));
					}
				}
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)permSet.Copy();
				namedPermissionSet.IgnoreTypeLoadFailures = true;
				this.m_namedPermissionSets.Add(namedPermissionSet);
			}
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x0009A4DC File Offset: 0x000986DC
		public NamedPermissionSet RemoveNamedPermissionSet(NamedPermissionSet permSet)
		{
			if (permSet == null)
			{
				throw new ArgumentNullException("permSet");
			}
			return this.RemoveNamedPermissionSet(permSet.Name);
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x0009A4F8 File Offset: 0x000986F8
		[SecuritySafeCritical]
		public NamedPermissionSet RemoveNamedPermissionSet(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.CheckLoaded();
			this.LoadAllPermissionSets();
			int num = -1;
			for (int i = 0; i < PolicyLevel.s_reservedNamedPermissionSets.Length; i++)
			{
				if (PolicyLevel.s_reservedNamedPermissionSets[i].Equals(name))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_ReservedNPMS", new object[]
					{
						name
					}));
				}
			}
			ArrayList namedPermissionSets = this.m_namedPermissionSets;
			for (int j = 0; j < namedPermissionSets.Count; j++)
			{
				if (((NamedPermissionSet)namedPermissionSets[j]).Name.Equals(name))
				{
					num = j;
					break;
				}
			}
			if (num == -1)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NoNPMS"));
			}
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.m_rootCodeGroup);
			for (int k = 0; k < arrayList.Count; k++)
			{
				CodeGroup codeGroup = (CodeGroup)arrayList[k];
				if (codeGroup.PermissionSetName != null && codeGroup.PermissionSetName.Equals(name))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_NPMSInUse", new object[]
					{
						name
					}));
				}
				IEnumerator enumerator = codeGroup.Children.GetEnumerator();
				if (enumerator != null)
				{
					while (enumerator.MoveNext())
					{
						object value = enumerator.Current;
						arrayList.Add(value);
					}
				}
			}
			NamedPermissionSet result = (NamedPermissionSet)namedPermissionSets[num];
			namedPermissionSets.RemoveAt(num);
			return result;
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x0009A65C File Offset: 0x0009885C
		[SecuritySafeCritical]
		public NamedPermissionSet ChangeNamedPermissionSet(string name, PermissionSet pSet)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (pSet == null)
			{
				throw new ArgumentNullException("pSet");
			}
			for (int i = 0; i < PolicyLevel.s_reservedNamedPermissionSets.Length; i++)
			{
				if (PolicyLevel.s_reservedNamedPermissionSets[i].Equals(name))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_ReservedNPMS", new object[]
					{
						name
					}));
				}
			}
			NamedPermissionSet namedPermissionSetInternal = this.GetNamedPermissionSetInternal(name);
			if (namedPermissionSetInternal == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NoNPMS"));
			}
			NamedPermissionSet result = (NamedPermissionSet)namedPermissionSetInternal.Copy();
			namedPermissionSetInternal.Reset();
			namedPermissionSetInternal.SetUnrestricted(pSet.IsUnrestricted());
			foreach (object obj in pSet)
			{
				namedPermissionSetInternal.SetPermission(((IPermission)obj).Copy());
			}
			if (pSet is NamedPermissionSet)
			{
				namedPermissionSetInternal.Description = ((NamedPermissionSet)pSet).Description;
			}
			return result;
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x0009A73C File Offset: 0x0009893C
		[SecuritySafeCritical]
		public NamedPermissionSet GetNamedPermissionSet(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			NamedPermissionSet namedPermissionSetInternal = this.GetNamedPermissionSetInternal(name);
			if (namedPermissionSetInternal != null)
			{
				return new NamedPermissionSet(namedPermissionSetInternal);
			}
			return null;
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x0009A76C File Offset: 0x0009896C
		[SecuritySafeCritical]
		public void Recover()
		{
			if (this.m_configId == ConfigId.None)
			{
				throw new PolicyException(Environment.GetResourceString("Policy_RecoverNotFileBased"));
			}
			lock (this)
			{
				if (!Config.RecoverData(this.m_configId))
				{
					throw new PolicyException(Environment.GetResourceString("Policy_RecoverNoConfigFile"));
				}
				this.m_loaded = false;
				this.m_rootCodeGroup = null;
				this.m_namedPermissionSets = null;
				this.m_fullTrustAssemblies = new ArrayList();
			}
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x0009A7F8 File Offset: 0x000989F8
		[SecuritySafeCritical]
		public void Reset()
		{
			this.SetDefault();
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x0009A800 File Offset: 0x00098A00
		[SecuritySafeCritical]
		public PolicyStatement Resolve(Evidence evidence)
		{
			return this.Resolve(evidence, 0, null);
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x0009A80C File Offset: 0x00098A0C
		[SecuritySafeCritical]
		public SecurityElement ToXml()
		{
			this.CheckLoaded();
			this.LoadAllPermissionSets();
			SecurityElement securityElement = new SecurityElement("PolicyLevel");
			securityElement.AddAttribute("version", "1");
			Hashtable hashtable = new Hashtable();
			lock (this)
			{
				SecurityElement securityElement2 = new SecurityElement("NamedPermissionSets");
				foreach (object obj in this.m_namedPermissionSets)
				{
					securityElement2.AddChild(this.NormalizeClassDeep(((NamedPermissionSet)obj).ToXml(), hashtable));
				}
				SecurityElement child = this.NormalizeClassDeep(this.m_rootCodeGroup.ToXml(this), hashtable);
				SecurityElement securityElement3 = new SecurityElement("FullTrustAssemblies");
				foreach (object obj2 in this.m_fullTrustAssemblies)
				{
					securityElement3.AddChild(this.NormalizeClassDeep(((StrongNameMembershipCondition)obj2).ToXml(), hashtable));
				}
				SecurityElement securityElement4 = new SecurityElement("SecurityClasses");
				IDictionaryEnumerator enumerator2 = hashtable.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					SecurityElement securityElement5 = new SecurityElement("SecurityClass");
					securityElement5.AddAttribute("Name", (string)enumerator2.Value);
					securityElement5.AddAttribute("Description", (string)enumerator2.Key);
					securityElement4.AddChild(securityElement5);
				}
				securityElement.AddChild(securityElement4);
				securityElement.AddChild(securityElement2);
				securityElement.AddChild(child);
				securityElement.AddChild(securityElement3);
			}
			return securityElement;
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x0009A998 File Offset: 0x00098B98
		public void FromXml(SecurityElement e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			lock (this)
			{
				ArrayList arrayList = new ArrayList();
				SecurityElement securityElement = e.SearchForChildByTag("SecurityClasses");
				Hashtable hashtable;
				if (securityElement != null)
				{
					hashtable = new Hashtable();
					foreach (object obj in securityElement.Children)
					{
						SecurityElement securityElement2 = (SecurityElement)obj;
						if (securityElement2.Tag.Equals("SecurityClass"))
						{
							string text = securityElement2.Attribute("Name");
							string text2 = securityElement2.Attribute("Description");
							if (text != null && text2 != null)
							{
								hashtable.Add(text, text2);
							}
						}
					}
				}
				else
				{
					hashtable = null;
				}
				SecurityElement securityElement3 = e.SearchForChildByTag("FullTrustAssemblies");
				if (securityElement3 != null && securityElement3.InternalChildren != null)
				{
					string assemblyQualifiedName = typeof(StrongNameMembershipCondition).AssemblyQualifiedName;
					IEnumerator enumerator2 = securityElement3.Children.GetEnumerator();
					while (enumerator2.MoveNext())
					{
						StrongNameMembershipCondition strongNameMembershipCondition = new StrongNameMembershipCondition();
						strongNameMembershipCondition.FromXml((SecurityElement)enumerator2.Current);
						arrayList.Add(strongNameMembershipCondition);
					}
				}
				this.m_fullTrustAssemblies = arrayList;
				ArrayList arrayList2 = new ArrayList();
				SecurityElement securityElement4 = e.SearchForChildByTag("NamedPermissionSets");
				SecurityElement securityElement5 = null;
				if (securityElement4 != null && securityElement4.InternalChildren != null)
				{
					securityElement5 = this.UnnormalizeClassDeep(securityElement4, hashtable);
					foreach (string name in PolicyLevel.s_reservedNamedPermissionSets)
					{
						this.FindElement(securityElement5, name);
					}
				}
				if (securityElement5 == null)
				{
					securityElement5 = new SecurityElement("NamedPermissionSets");
				}
				arrayList2.Add(BuiltInPermissionSets.FullTrust);
				arrayList2.Add(BuiltInPermissionSets.Everything);
				arrayList2.Add(BuiltInPermissionSets.SkipVerification);
				arrayList2.Add(BuiltInPermissionSets.Execution);
				arrayList2.Add(BuiltInPermissionSets.Nothing);
				arrayList2.Add(BuiltInPermissionSets.Internet);
				arrayList2.Add(BuiltInPermissionSets.LocalIntranet);
				foreach (object obj2 in arrayList2)
				{
					PermissionSet permissionSet = (PermissionSet)obj2;
					permissionSet.IgnoreTypeLoadFailures = true;
				}
				this.m_namedPermissionSets = arrayList2;
				this.m_permSetElement = securityElement5;
				SecurityElement securityElement6 = e.SearchForChildByTag("CodeGroup");
				if (securityElement6 == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLElement", new object[]
					{
						"CodeGroup",
						base.GetType().FullName
					}));
				}
				CodeGroup codeGroup = XMLUtil.CreateCodeGroup(this.UnnormalizeClassDeep(securityElement6, hashtable));
				if (codeGroup == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLElement", new object[]
					{
						"CodeGroup",
						base.GetType().FullName
					}));
				}
				codeGroup.FromXml(securityElement6, this);
				this.m_rootCodeGroup = codeGroup;
			}
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x0009AC9C File Offset: 0x00098E9C
		[SecurityCritical]
		internal static PermissionSet GetBuiltInSet(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			if (name.Equals("FullTrust"))
			{
				return BuiltInPermissionSets.FullTrust;
			}
			if (name.Equals("Nothing"))
			{
				return BuiltInPermissionSets.Nothing;
			}
			if (name.Equals("Execution"))
			{
				return BuiltInPermissionSets.Execution;
			}
			if (name.Equals("SkipVerification"))
			{
				return BuiltInPermissionSets.SkipVerification;
			}
			if (name.Equals("Internet"))
			{
				return BuiltInPermissionSets.Internet;
			}
			if (name.Equals("LocalIntranet"))
			{
				return BuiltInPermissionSets.LocalIntranet;
			}
			return null;
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x0009AD28 File Offset: 0x00098F28
		[SecurityCritical]
		internal NamedPermissionSet GetNamedPermissionSetInternal(string name)
		{
			this.CheckLoaded();
			object internalSyncObject = PolicyLevel.InternalSyncObject;
			lock (internalSyncObject)
			{
				foreach (object obj in this.m_namedPermissionSets)
				{
					NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
					if (namedPermissionSet.Name.Equals(name))
					{
						return namedPermissionSet;
					}
				}
				if (this.m_permSetElement != null)
				{
					SecurityElement securityElement = this.FindElement(this.m_permSetElement, name);
					if (securityElement != null)
					{
						NamedPermissionSet namedPermissionSet2 = new NamedPermissionSet();
						namedPermissionSet2.Name = name;
						this.m_namedPermissionSets.Add(namedPermissionSet2);
						try
						{
							namedPermissionSet2.FromXml(securityElement, false, true);
						}
						catch
						{
							this.m_namedPermissionSets.Remove(namedPermissionSet2);
							return null;
						}
						if (namedPermissionSet2.Name != null)
						{
							return namedPermissionSet2;
						}
						this.m_namedPermissionSets.Remove(namedPermissionSet2);
					}
				}
			}
			return null;
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x0009AE48 File Offset: 0x00099048
		[SecurityCritical]
		internal PolicyStatement Resolve(Evidence evidence, int count, byte[] serializedEvidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			PolicyStatement policyStatement = null;
			if (serializedEvidence != null)
			{
				policyStatement = this.CheckCache(count, serializedEvidence);
			}
			if (policyStatement == null)
			{
				this.CheckLoaded();
				bool flag = this.m_fullTrustAssemblies != null && PolicyLevel.IsFullTrustAssembly(this.m_fullTrustAssemblies, evidence);
				bool flag2;
				if (flag)
				{
					policyStatement = new PolicyStatement(new PermissionSet(true), PolicyStatementAttribute.Nothing);
					flag2 = true;
				}
				else
				{
					ArrayList arrayList = this.GenericResolve(evidence, out flag2);
					policyStatement = new PolicyStatement();
					policyStatement.PermissionSet = null;
					foreach (object obj in arrayList)
					{
						PolicyStatement policy = ((CodeGroupStackFrame)obj).policy;
						if (policy != null)
						{
							policyStatement.GetPermissionSetNoCopy().InplaceUnion(policy.GetPermissionSetNoCopy());
							policyStatement.Attributes |= policy.Attributes;
							if (policy.HasDependentEvidence)
							{
								foreach (IDelayEvaluatedEvidence delayEvaluatedEvidence in policy.DependentEvidence)
								{
									delayEvaluatedEvidence.MarkUsed();
								}
							}
						}
					}
				}
				if (flag2)
				{
					this.Cache(count, evidence.RawSerialize(), policyStatement);
				}
			}
			return policyStatement;
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x0009AF78 File Offset: 0x00099178
		[SecurityCritical]
		private void CheckLoaded()
		{
			if (!this.m_loaded)
			{
				object internalSyncObject = PolicyLevel.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (!this.m_loaded)
					{
						this.LoadPolicyLevel();
					}
				}
			}
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x0009AFC8 File Offset: 0x000991C8
		private static byte[] ReadFile(string fileName)
		{
			byte[] result;
			using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				int num = (int)fileStream.Length;
				byte[] array = new byte[num];
				num = fileStream.Read(array, 0, num);
				fileStream.Close();
				result = array;
			}
			return result;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x0009B01C File Offset: 0x0009921C
		[SecurityCritical]
		private void LoadPolicyLevel()
		{
			Exception ex = null;
			CodeAccessPermission.Assert(true);
			if (File.InternalExists(this.m_path))
			{
				Encoding utf = Encoding.UTF8;
				SecurityElement securityElement;
				try
				{
					string @string = utf.GetString(PolicyLevel.ReadFile(this.m_path));
					securityElement = SecurityElement.FromString(@string);
				}
				catch (Exception ex2)
				{
					string text;
					if (!string.IsNullOrEmpty(ex2.Message))
					{
						text = ex2.Message;
					}
					else
					{
						text = ex2.GetType().AssemblyQualifiedName;
					}
					ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParseEx", new object[]
					{
						this.Label,
						text
					}));
					goto IL_1BD;
				}
				if (securityElement == null)
				{
					ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[]
					{
						this.Label
					}));
				}
				else
				{
					SecurityElement securityElement2 = securityElement.SearchForChildByTag("mscorlib");
					if (securityElement2 == null)
					{
						ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[]
						{
							this.Label
						}));
					}
					else
					{
						SecurityElement securityElement3 = securityElement2.SearchForChildByTag("security");
						if (securityElement3 == null)
						{
							ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[]
							{
								this.Label
							}));
						}
						else
						{
							SecurityElement securityElement4 = securityElement3.SearchForChildByTag("policy");
							if (securityElement4 == null)
							{
								ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[]
								{
									this.Label
								}));
							}
							else
							{
								SecurityElement securityElement5 = securityElement4.SearchForChildByTag("PolicyLevel");
								if (securityElement5 != null)
								{
									try
									{
										this.FromXml(securityElement5);
										goto IL_1B5;
									}
									catch (Exception)
									{
										ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[]
										{
											this.Label
										}));
										goto IL_1BD;
									}
									goto IL_193;
									IL_1B5:
									this.m_loaded = true;
									return;
								}
								IL_193:
								ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[]
								{
									this.Label
								}));
							}
						}
					}
				}
			}
			IL_1BD:
			this.SetDefault();
			this.m_loaded = true;
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x0009B214 File Offset: 0x00099414
		[SecurityCritical]
		private Exception LoadError(string message)
		{
			if (this.m_type != PolicyLevelType.User && this.m_type != PolicyLevelType.Machine && this.m_type != PolicyLevelType.Enterprise)
			{
				return new ArgumentException(message);
			}
			Config.WriteToEventLog(message);
			return null;
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x0009B240 File Offset: 0x00099440
		[SecurityCritical]
		private void Cache(int count, byte[] serializedEvidence, PolicyStatement policy)
		{
			if (this.m_configId == ConfigId.None)
			{
				return;
			}
			if (serializedEvidence == null)
			{
				return;
			}
			byte[] data = new SecurityDocument(policy.ToXml(null, true)).m_data;
			Config.AddCacheEntry(this.m_configId, count, serializedEvidence, data);
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x0009B27C File Offset: 0x0009947C
		[SecurityCritical]
		private PolicyStatement CheckCache(int count, byte[] serializedEvidence)
		{
			if (this.m_configId == ConfigId.None)
			{
				return null;
			}
			if (serializedEvidence == null)
			{
				return null;
			}
			byte[] data;
			if (!Config.GetCacheEntry(this.m_configId, count, serializedEvidence, out data))
			{
				return null;
			}
			PolicyStatement policyStatement = new PolicyStatement();
			SecurityDocument doc = new SecurityDocument(data);
			policyStatement.FromXml(doc, 0, null, true);
			return policyStatement;
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x0009B2C4 File Offset: 0x000994C4
		[SecurityCritical]
		private static bool IsFullTrustAssembly(ArrayList fullTrustAssemblies, Evidence evidence)
		{
			if (fullTrustAssemblies.Count == 0)
			{
				return false;
			}
			if (evidence != null)
			{
				lock (fullTrustAssemblies)
				{
					foreach (object obj in fullTrustAssemblies)
					{
						StrongNameMembershipCondition strongNameMembershipCondition = (StrongNameMembershipCondition)obj;
						if (strongNameMembershipCondition.Check(evidence))
						{
							if (Environment.GetCompatibilityFlag(CompatibilityFlag.FullTrustListAssembliesInGac))
							{
								if (new ZoneMembershipCondition().Check(evidence))
								{
									return true;
								}
							}
							else if (new GacMembershipCondition().Check(evidence))
							{
								return true;
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x0009B35C File Offset: 0x0009955C
		private CodeGroup CreateDefaultAllGroup()
		{
			UnionCodeGroup unionCodeGroup = new UnionCodeGroup();
			unionCodeGroup.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new AllMembershipCondition().ToXml()), this);
			unionCodeGroup.Name = Environment.GetResourceString("Policy_AllCode_Name");
			unionCodeGroup.Description = Environment.GetResourceString("Policy_AllCode_DescriptionFullTrust");
			return unionCodeGroup;
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x0009B3B0 File Offset: 0x000995B0
		[SecurityCritical]
		private CodeGroup CreateDefaultMachinePolicy()
		{
			UnionCodeGroup unionCodeGroup = new UnionCodeGroup();
			unionCodeGroup.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Nothing", new AllMembershipCondition().ToXml()), this);
			unionCodeGroup.Name = Environment.GetResourceString("Policy_AllCode_Name");
			unionCodeGroup.Description = Environment.GetResourceString("Policy_AllCode_DescriptionNothing");
			UnionCodeGroup unionCodeGroup2 = new UnionCodeGroup();
			unionCodeGroup2.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new ZoneMembershipCondition(SecurityZone.MyComputer).ToXml()), this);
			unionCodeGroup2.Name = Environment.GetResourceString("Policy_MyComputer_Name");
			unionCodeGroup2.Description = Environment.GetResourceString("Policy_MyComputer_Description");
			StrongNamePublicKeyBlob blob = new StrongNamePublicKeyBlob("002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293");
			UnionCodeGroup unionCodeGroup3 = new UnionCodeGroup();
			unionCodeGroup3.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new StrongNameMembershipCondition(blob, null, null).ToXml()), this);
			unionCodeGroup3.Name = Environment.GetResourceString("Policy_Microsoft_Name");
			unionCodeGroup3.Description = Environment.GetResourceString("Policy_Microsoft_Description");
			unionCodeGroup2.AddChildInternal(unionCodeGroup3);
			blob = new StrongNamePublicKeyBlob("00000000000000000400000000000000");
			UnionCodeGroup unionCodeGroup4 = new UnionCodeGroup();
			unionCodeGroup4.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new StrongNameMembershipCondition(blob, null, null).ToXml()), this);
			unionCodeGroup4.Name = Environment.GetResourceString("Policy_Ecma_Name");
			unionCodeGroup4.Description = Environment.GetResourceString("Policy_Ecma_Description");
			unionCodeGroup2.AddChildInternal(unionCodeGroup4);
			unionCodeGroup.AddChildInternal(unionCodeGroup2);
			CodeGroup codeGroup = new UnionCodeGroup();
			codeGroup.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "LocalIntranet", new ZoneMembershipCondition(SecurityZone.Intranet).ToXml()), this);
			codeGroup.Name = Environment.GetResourceString("Policy_Intranet_Name");
			codeGroup.Description = Environment.GetResourceString("Policy_Intranet_Description");
			codeGroup.AddChildInternal(new NetCodeGroup(new AllMembershipCondition())
			{
				Name = Environment.GetResourceString("Policy_IntranetNet_Name"),
				Description = Environment.GetResourceString("Policy_IntranetNet_Description")
			});
			codeGroup.AddChildInternal(new FileCodeGroup(new AllMembershipCondition(), FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery)
			{
				Name = Environment.GetResourceString("Policy_IntranetFile_Name"),
				Description = Environment.GetResourceString("Policy_IntranetFile_Description")
			});
			unionCodeGroup.AddChildInternal(codeGroup);
			CodeGroup codeGroup2 = new UnionCodeGroup();
			codeGroup2.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Internet", new ZoneMembershipCondition(SecurityZone.Internet).ToXml()), this);
			codeGroup2.Name = Environment.GetResourceString("Policy_Internet_Name");
			codeGroup2.Description = Environment.GetResourceString("Policy_Internet_Description");
			codeGroup2.AddChildInternal(new NetCodeGroup(new AllMembershipCondition())
			{
				Name = Environment.GetResourceString("Policy_InternetNet_Name"),
				Description = Environment.GetResourceString("Policy_InternetNet_Description")
			});
			unionCodeGroup.AddChildInternal(codeGroup2);
			CodeGroup codeGroup3 = new UnionCodeGroup();
			codeGroup3.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Nothing", new ZoneMembershipCondition(SecurityZone.Untrusted).ToXml()), this);
			codeGroup3.Name = Environment.GetResourceString("Policy_Untrusted_Name");
			codeGroup3.Description = Environment.GetResourceString("Policy_Untrusted_Description");
			unionCodeGroup.AddChildInternal(codeGroup3);
			CodeGroup codeGroup4 = new UnionCodeGroup();
			codeGroup4.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Internet", new ZoneMembershipCondition(SecurityZone.Trusted).ToXml()), this);
			codeGroup4.Name = Environment.GetResourceString("Policy_Trusted_Name");
			codeGroup4.Description = Environment.GetResourceString("Policy_Trusted_Description");
			codeGroup4.AddChildInternal(new NetCodeGroup(new AllMembershipCondition())
			{
				Name = Environment.GetResourceString("Policy_TrustedNet_Name"),
				Description = Environment.GetResourceString("Policy_TrustedNet_Description")
			});
			unionCodeGroup.AddChildInternal(codeGroup4);
			return unionCodeGroup;
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x0009B738 File Offset: 0x00099938
		private static SecurityElement CreateCodeGroupElement(string codeGroupType, string permissionSetName, SecurityElement mshipElement)
		{
			SecurityElement securityElement = new SecurityElement("CodeGroup");
			securityElement.AddAttribute("class", ("System.Security." + codeGroupType + ", mscorlib, Version={VERSION}, Culture=neutral, PublicKeyToken=b77a5c561934e089") ?? "");
			securityElement.AddAttribute("version", "1");
			securityElement.AddAttribute("PermissionSetName", permissionSetName);
			securityElement.AddChild(mshipElement);
			return securityElement;
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x0009B798 File Offset: 0x00099998
		private void SetDefaultFullTrustAssemblies()
		{
			this.m_fullTrustAssemblies = new ArrayList();
			StrongNamePublicKeyBlob blob = new StrongNamePublicKeyBlob("00000000000000000400000000000000");
			for (int i = 0; i < PolicyLevel.EcmaFullTrustAssemblies.Length; i++)
			{
				StrongNameMembershipCondition value = new StrongNameMembershipCondition(blob, PolicyLevel.EcmaFullTrustAssemblies[i], new Version("4.0.0.0"));
				this.m_fullTrustAssemblies.Add(value);
			}
			StrongNamePublicKeyBlob blob2 = new StrongNamePublicKeyBlob("002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293");
			for (int j = 0; j < PolicyLevel.MicrosoftFullTrustAssemblies.Length; j++)
			{
				StrongNameMembershipCondition value2 = new StrongNameMembershipCondition(blob2, PolicyLevel.MicrosoftFullTrustAssemblies[j], new Version("4.0.0.0"));
				this.m_fullTrustAssemblies.Add(value2);
			}
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x0009B83C File Offset: 0x00099A3C
		[SecurityCritical]
		private void SetDefault()
		{
			lock (this)
			{
				string path = PolicyLevel.GetLocationFromType(this.m_type) + ".default";
				if (File.InternalExists(path))
				{
					PolicyLevel policyLevel = new PolicyLevel(this.m_type, path);
					this.m_rootCodeGroup = policyLevel.RootCodeGroup;
					this.m_namedPermissionSets = (ArrayList)policyLevel.NamedPermissionSets;
					this.m_fullTrustAssemblies = (ArrayList)policyLevel.FullTrustAssemblies;
					this.m_loaded = true;
				}
				else
				{
					this.m_namedPermissionSets = null;
					this.m_rootCodeGroup = null;
					this.m_permSetElement = null;
					this.m_rootCodeGroup = ((this.m_type == PolicyLevelType.Machine) ? this.CreateDefaultMachinePolicy() : this.CreateDefaultAllGroup());
					this.SetFactoryPermissionSets();
					this.SetDefaultFullTrustAssemblies();
					this.m_loaded = true;
				}
			}
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x0009B918 File Offset: 0x00099B18
		private void SetFactoryPermissionSets()
		{
			object internalSyncObject = PolicyLevel.InternalSyncObject;
			lock (internalSyncObject)
			{
				this.m_namedPermissionSets = new ArrayList();
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.FullTrust);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.Everything);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.Nothing);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.SkipVerification);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.Execution);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.Internet);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.LocalIntranet);
			}
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x0009B9D4 File Offset: 0x00099BD4
		private SecurityElement FindElement(SecurityElement element, string name)
		{
			foreach (object obj in element.Children)
			{
				SecurityElement securityElement = (SecurityElement)obj;
				if (securityElement.Tag.Equals("PermissionSet"))
				{
					string text = securityElement.Attribute("Name");
					if (text != null && text.Equals(name))
					{
						element.InternalChildren.Remove(securityElement);
						return securityElement;
					}
				}
			}
			return null;
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x0009BA3C File Offset: 0x00099C3C
		[SecurityCritical]
		private void LoadAllPermissionSets()
		{
			if (this.m_permSetElement != null && this.m_permSetElement.InternalChildren != null)
			{
				object internalSyncObject = PolicyLevel.InternalSyncObject;
				lock (internalSyncObject)
				{
					while (this.m_permSetElement != null && this.m_permSetElement.InternalChildren.Count != 0)
					{
						SecurityElement securityElement = (SecurityElement)this.m_permSetElement.Children[this.m_permSetElement.InternalChildren.Count - 1];
						this.m_permSetElement.InternalChildren.RemoveAt(this.m_permSetElement.InternalChildren.Count - 1);
						if (securityElement.Tag.Equals("PermissionSet") && securityElement.Attribute("class").Equals("System.Security.NamedPermissionSet"))
						{
							NamedPermissionSet namedPermissionSet = new NamedPermissionSet();
							namedPermissionSet.FromXmlNameOnly(securityElement);
							if (namedPermissionSet.Name != null)
							{
								this.m_namedPermissionSets.Add(namedPermissionSet);
								try
								{
									namedPermissionSet.FromXml(securityElement, false, true);
								}
								catch
								{
									this.m_namedPermissionSets.Remove(namedPermissionSet);
								}
							}
						}
					}
					this.m_permSetElement = null;
				}
			}
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x0009BB74 File Offset: 0x00099D74
		[SecurityCritical]
		private ArrayList GenericResolve(Evidence evidence, out bool allConst)
		{
			CodeGroupStack codeGroupStack = new CodeGroupStack();
			CodeGroup rootCodeGroup = this.m_rootCodeGroup;
			if (rootCodeGroup == null)
			{
				throw new PolicyException(Environment.GetResourceString("Policy_NonFullTrustAssembly"));
			}
			CodeGroupStackFrame codeGroupStackFrame = new CodeGroupStackFrame();
			codeGroupStackFrame.current = rootCodeGroup;
			codeGroupStackFrame.parent = null;
			codeGroupStack.Push(codeGroupStackFrame);
			ArrayList arrayList = new ArrayList();
			bool flag = false;
			allConst = true;
			Exception ex = null;
			while (!codeGroupStack.IsEmpty())
			{
				codeGroupStackFrame = codeGroupStack.Pop();
				FirstMatchCodeGroup firstMatchCodeGroup = codeGroupStackFrame.current as FirstMatchCodeGroup;
				UnionCodeGroup unionCodeGroup = codeGroupStackFrame.current as UnionCodeGroup;
				if (!(codeGroupStackFrame.current.MembershipCondition is IConstantMembershipCondition) || (unionCodeGroup == null && firstMatchCodeGroup == null))
				{
					allConst = false;
				}
				try
				{
					codeGroupStackFrame.policy = PolicyManager.ResolveCodeGroup(codeGroupStackFrame.current, evidence);
				}
				catch (Exception ex2)
				{
					if (ex == null)
					{
						ex = ex2;
					}
				}
				if (codeGroupStackFrame.policy != null)
				{
					if ((codeGroupStackFrame.policy.Attributes & PolicyStatementAttribute.Exclusive) != PolicyStatementAttribute.Nothing)
					{
						if (flag)
						{
							throw new PolicyException(Environment.GetResourceString("Policy_MultipleExclusive"));
						}
						arrayList.RemoveRange(0, arrayList.Count);
						arrayList.Add(codeGroupStackFrame);
						flag = true;
					}
					if (!flag)
					{
						arrayList.Add(codeGroupStackFrame);
					}
				}
			}
			if (ex != null)
			{
				throw ex;
			}
			return arrayList;
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x0009BCA4 File Offset: 0x00099EA4
		private static string GenerateFriendlyName(string className, Hashtable classes)
		{
			if (classes.ContainsKey(className))
			{
				return (string)classes[className];
			}
			Type type = System.Type.GetType(className, false, false);
			if (type != null && !type.IsVisible)
			{
				type = null;
			}
			if (type == null)
			{
				return className;
			}
			if (!classes.ContainsValue(type.Name))
			{
				classes.Add(className, type.Name);
				return type.Name;
			}
			if (!classes.ContainsValue(type.FullName))
			{
				classes.Add(className, type.FullName);
				return type.FullName;
			}
			classes.Add(className, type.AssemblyQualifiedName);
			return type.AssemblyQualifiedName;
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x0009BD48 File Offset: 0x00099F48
		private SecurityElement NormalizeClassDeep(SecurityElement elem, Hashtable classes)
		{
			this.NormalizeClass(elem, classes);
			if (elem.InternalChildren != null && elem.InternalChildren.Count > 0)
			{
				foreach (object obj in elem.Children)
				{
					this.NormalizeClassDeep((SecurityElement)obj, classes);
				}
			}
			return elem;
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x0009BDA0 File Offset: 0x00099FA0
		private SecurityElement NormalizeClass(SecurityElement elem, Hashtable classes)
		{
			if (elem.m_lAttributes == null || elem.m_lAttributes.Count == 0)
			{
				return elem;
			}
			int count = elem.m_lAttributes.Count;
			for (int i = 0; i < count; i += 2)
			{
				string text = (string)elem.m_lAttributes[i];
				if (text.Equals("class"))
				{
					string className = (string)elem.m_lAttributes[i + 1];
					elem.m_lAttributes[i + 1] = PolicyLevel.GenerateFriendlyName(className, classes);
					break;
				}
			}
			return elem;
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x0009BE28 File Offset: 0x0009A028
		private SecurityElement UnnormalizeClassDeep(SecurityElement elem, Hashtable classes)
		{
			this.UnnormalizeClass(elem, classes);
			if (elem.InternalChildren != null && elem.InternalChildren.Count > 0)
			{
				foreach (object obj in elem.Children)
				{
					this.UnnormalizeClassDeep((SecurityElement)obj, classes);
				}
			}
			return elem;
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x0009BE80 File Offset: 0x0009A080
		private SecurityElement UnnormalizeClass(SecurityElement elem, Hashtable classes)
		{
			if (classes == null || elem.m_lAttributes == null || elem.m_lAttributes.Count == 0)
			{
				return elem;
			}
			int count = elem.m_lAttributes.Count;
			int i = 0;
			while (i < count)
			{
				string text = (string)elem.m_lAttributes[i];
				if (text.Equals("class"))
				{
					string key = (string)elem.m_lAttributes[i + 1];
					string text2 = (string)classes[key];
					if (text2 != null)
					{
						elem.m_lAttributes[i + 1] = text2;
						break;
					}
					break;
				}
				else
				{
					i += 2;
				}
			}
			return elem;
		}

		// Token: 0x040010BF RID: 4287
		private ArrayList m_fullTrustAssemblies;

		// Token: 0x040010C0 RID: 4288
		private ArrayList m_namedPermissionSets;

		// Token: 0x040010C1 RID: 4289
		private CodeGroup m_rootCodeGroup;

		// Token: 0x040010C2 RID: 4290
		private string m_label;

		// Token: 0x040010C3 RID: 4291
		[OptionalField(VersionAdded = 2)]
		private PolicyLevelType m_type;

		// Token: 0x040010C4 RID: 4292
		private ConfigId m_configId;

		// Token: 0x040010C5 RID: 4293
		private bool m_useDefaultCodeGroupsOnReset;

		// Token: 0x040010C6 RID: 4294
		private bool m_generateQuickCacheOnLoad;

		// Token: 0x040010C7 RID: 4295
		private bool m_caching;

		// Token: 0x040010C8 RID: 4296
		private bool m_throwOnLoadError;

		// Token: 0x040010C9 RID: 4297
		private Encoding m_encoding;

		// Token: 0x040010CA RID: 4298
		private bool m_loaded;

		// Token: 0x040010CB RID: 4299
		private SecurityElement m_permSetElement;

		// Token: 0x040010CC RID: 4300
		private string m_path;

		// Token: 0x040010CD RID: 4301
		private static object s_InternalSyncObject;

		// Token: 0x040010CE RID: 4302
		private static readonly string[] s_reservedNamedPermissionSets = new string[]
		{
			"FullTrust",
			"Nothing",
			"Execution",
			"SkipVerification",
			"Internet",
			"LocalIntranet",
			"Everything"
		};

		// Token: 0x040010CF RID: 4303
		private static string[] EcmaFullTrustAssemblies = new string[]
		{
			"mscorlib.resources",
			"System",
			"System.resources",
			"System.Xml",
			"System.Xml.resources",
			"System.Windows.Forms",
			"System.Windows.Forms.resources",
			"System.Data",
			"System.Data.resources"
		};

		// Token: 0x040010D0 RID: 4304
		private static string[] MicrosoftFullTrustAssemblies = new string[]
		{
			"System.Security",
			"System.Security.resources",
			"System.Drawing",
			"System.Drawing.resources",
			"System.Messaging",
			"System.Messaging.resources",
			"System.ServiceProcess",
			"System.ServiceProcess.resources",
			"System.DirectoryServices",
			"System.DirectoryServices.resources",
			"System.Deployment",
			"System.Deployment.resources"
		};
	}
}
