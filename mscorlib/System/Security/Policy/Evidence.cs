using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Threading;

namespace System.Security.Policy
{
	// Token: 0x02000320 RID: 800
	[ComVisible(true)]
	[Serializable]
	public sealed class Evidence : ICollection, IEnumerable
	{
		// Token: 0x060028D9 RID: 10457 RVA: 0x00096AB1 File Offset: 0x00094CB1
		public Evidence()
		{
			this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
			this.m_evidenceLock = new ReaderWriterLock();
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x00096AD0 File Offset: 0x00094CD0
		public Evidence(Evidence evidence)
		{
			this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
			if (evidence != null)
			{
				using (new Evidence.EvidenceLockHolder(evidence, Evidence.EvidenceLockHolder.LockType.Reader))
				{
					foreach (KeyValuePair<Type, EvidenceTypeDescriptor> keyValuePair in evidence.m_evidence)
					{
						EvidenceTypeDescriptor evidenceTypeDescriptor = keyValuePair.Value;
						if (evidenceTypeDescriptor != null)
						{
							evidenceTypeDescriptor = evidenceTypeDescriptor.Clone();
						}
						this.m_evidence[keyValuePair.Key] = evidenceTypeDescriptor;
					}
					this.m_target = evidence.m_target;
					this.m_locked = evidence.m_locked;
					this.m_deserializedTargetEvidence = evidence.m_deserializedTargetEvidence;
					if (evidence.Target != null)
					{
						this.m_cloneOrigin = new WeakReference(evidence);
					}
				}
			}
			this.m_evidenceLock = new ReaderWriterLock();
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x00096BBC File Offset: 0x00094DBC
		[Obsolete("This constructor is obsolete. Please use the constructor which takes arrays of EvidenceBase instead.")]
		public Evidence(object[] hostEvidence, object[] assemblyEvidence)
		{
			this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
			if (hostEvidence != null)
			{
				foreach (object id in hostEvidence)
				{
					this.AddHost(id);
				}
			}
			if (assemblyEvidence != null)
			{
				foreach (object id2 in assemblyEvidence)
				{
					this.AddAssembly(id2);
				}
			}
			this.m_evidenceLock = new ReaderWriterLock();
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x00096C28 File Offset: 0x00094E28
		public Evidence(EvidenceBase[] hostEvidence, EvidenceBase[] assemblyEvidence)
		{
			this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
			if (hostEvidence != null)
			{
				foreach (EvidenceBase evidence in hostEvidence)
				{
					this.AddHostEvidence(evidence, Evidence.GetEvidenceIndexType(evidence), Evidence.DuplicateEvidenceAction.Throw);
				}
			}
			if (assemblyEvidence != null)
			{
				foreach (EvidenceBase evidence2 in assemblyEvidence)
				{
					this.AddAssemblyEvidence(evidence2, Evidence.GetEvidenceIndexType(evidence2), Evidence.DuplicateEvidenceAction.Throw);
				}
			}
			this.m_evidenceLock = new ReaderWriterLock();
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x00096CA4 File Offset: 0x00094EA4
		[SecuritySafeCritical]
		internal Evidence(IRuntimeEvidenceFactory target)
		{
			this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
			this.m_target = target;
			foreach (Type key in Evidence.RuntimeEvidenceTypes)
			{
				this.m_evidence[key] = null;
			}
			this.QueryHostForPossibleEvidenceTypes();
			this.m_evidenceLock = new ReaderWriterLock();
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060028DE RID: 10462 RVA: 0x00096D00 File Offset: 0x00094F00
		internal static Type[] RuntimeEvidenceTypes
		{
			get
			{
				if (Evidence.s_runtimeEvidenceTypes == null)
				{
					Type[] array = new Type[]
					{
						typeof(ActivationArguments),
						typeof(ApplicationDirectory),
						typeof(ApplicationTrust),
						typeof(GacInstalled),
						typeof(Hash),
						typeof(Publisher),
						typeof(Site),
						typeof(StrongName),
						typeof(Url),
						typeof(Zone)
					};
					if (AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
					{
						int num = array.Length;
						Array.Resize<Type>(ref array, num + 1);
						array[num] = typeof(PermissionRequestEvidence);
					}
					Evidence.s_runtimeEvidenceTypes = array;
				}
				return Evidence.s_runtimeEvidenceTypes;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060028DF RID: 10463 RVA: 0x00096DDA File Offset: 0x00094FDA
		private bool IsReaderLockHeld
		{
			get
			{
				return this.m_evidenceLock == null || this.m_evidenceLock.IsReaderLockHeld;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060028E0 RID: 10464 RVA: 0x00096DF1 File Offset: 0x00094FF1
		private bool IsWriterLockHeld
		{
			get
			{
				return this.m_evidenceLock == null || this.m_evidenceLock.IsWriterLockHeld;
			}
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x00096E08 File Offset: 0x00095008
		private void AcquireReaderLock()
		{
			if (this.m_evidenceLock != null)
			{
				this.m_evidenceLock.AcquireReaderLock(5000);
			}
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x00096E22 File Offset: 0x00095022
		private void AcquireWriterlock()
		{
			if (this.m_evidenceLock != null)
			{
				this.m_evidenceLock.AcquireWriterLock(5000);
			}
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x00096E3C File Offset: 0x0009503C
		private void DowngradeFromWriterLock(ref LockCookie lockCookie)
		{
			if (this.m_evidenceLock != null)
			{
				this.m_evidenceLock.DowngradeFromWriterLock(ref lockCookie);
			}
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x00096E54 File Offset: 0x00095054
		private LockCookie UpgradeToWriterLock()
		{
			if (this.m_evidenceLock == null)
			{
				return default(LockCookie);
			}
			return this.m_evidenceLock.UpgradeToWriterLock(5000);
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x00096E83 File Offset: 0x00095083
		private void ReleaseReaderLock()
		{
			if (this.m_evidenceLock != null)
			{
				this.m_evidenceLock.ReleaseReaderLock();
			}
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x00096E98 File Offset: 0x00095098
		private void ReleaseWriterLock()
		{
			if (this.m_evidenceLock != null)
			{
				this.m_evidenceLock.ReleaseWriterLock();
			}
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x00096EB0 File Offset: 0x000950B0
		[Obsolete("This method is obsolete. Please use AddHostEvidence instead.")]
		[SecuritySafeCritical]
		public void AddHost(object id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (!id.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Policy_EvidenceMustBeSerializable"), "id");
			}
			if (this.m_locked)
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
			}
			EvidenceBase evidence = Evidence.WrapLegacyEvidence(id);
			Type evidenceIndexType = Evidence.GetEvidenceIndexType(evidence);
			this.AddHostEvidence(evidence, evidenceIndexType, Evidence.DuplicateEvidenceAction.Merge);
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x00096F18 File Offset: 0x00095118
		[Obsolete("This method is obsolete. Please use AddAssemblyEvidence instead.")]
		public void AddAssembly(object id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (!id.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Policy_EvidenceMustBeSerializable"), "id");
			}
			EvidenceBase evidence = Evidence.WrapLegacyEvidence(id);
			Type evidenceIndexType = Evidence.GetEvidenceIndexType(evidence);
			this.AddAssemblyEvidence(evidence, evidenceIndexType, Evidence.DuplicateEvidenceAction.Merge);
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x00096F6C File Offset: 0x0009516C
		[ComVisible(false)]
		public void AddAssemblyEvidence<T>(T evidence) where T : EvidenceBase
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			Type evidenceType = typeof(T);
			if (typeof(T) == typeof(EvidenceBase) || evidence is ILegacyEvidenceAdapter)
			{
				evidenceType = Evidence.GetEvidenceIndexType(evidence);
			}
			this.AddAssemblyEvidence(evidence, evidenceType, Evidence.DuplicateEvidenceAction.Throw);
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x00096FDC File Offset: 0x000951DC
		private void AddAssemblyEvidence(EvidenceBase evidence, Type evidenceType, Evidence.DuplicateEvidenceAction duplicateAction)
		{
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
			{
				this.AddAssemblyEvidenceNoLock(evidence, evidenceType, duplicateAction);
			}
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x00097018 File Offset: 0x00095218
		private void AddAssemblyEvidenceNoLock(EvidenceBase evidence, Type evidenceType, Evidence.DuplicateEvidenceAction duplicateAction)
		{
			this.DeserializeTargetEvidence();
			EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(evidenceType, true);
			this.m_version += 1U;
			if (evidenceTypeDescriptor.AssemblyEvidence == null)
			{
				evidenceTypeDescriptor.AssemblyEvidence = evidence;
				return;
			}
			evidenceTypeDescriptor.AssemblyEvidence = Evidence.HandleDuplicateEvidence(evidenceTypeDescriptor.AssemblyEvidence, evidence, duplicateAction);
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x00097068 File Offset: 0x00095268
		[ComVisible(false)]
		public void AddHostEvidence<T>(T evidence) where T : EvidenceBase
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			Type evidenceType = typeof(T);
			if (typeof(T) == typeof(EvidenceBase) || evidence is ILegacyEvidenceAdapter)
			{
				evidenceType = Evidence.GetEvidenceIndexType(evidence);
			}
			this.AddHostEvidence(evidence, evidenceType, Evidence.DuplicateEvidenceAction.Throw);
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x000970D8 File Offset: 0x000952D8
		[SecuritySafeCritical]
		private void AddHostEvidence(EvidenceBase evidence, Type evidenceType, Evidence.DuplicateEvidenceAction duplicateAction)
		{
			if (this.Locked)
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
			}
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
			{
				this.AddHostEvidenceNoLock(evidence, evidenceType, duplicateAction);
			}
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x00097128 File Offset: 0x00095328
		private void AddHostEvidenceNoLock(EvidenceBase evidence, Type evidenceType, Evidence.DuplicateEvidenceAction duplicateAction)
		{
			EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(evidenceType, true);
			this.m_version += 1U;
			if (evidenceTypeDescriptor.HostEvidence == null)
			{
				evidenceTypeDescriptor.HostEvidence = evidence;
				return;
			}
			evidenceTypeDescriptor.HostEvidence = Evidence.HandleDuplicateEvidence(evidenceTypeDescriptor.HostEvidence, evidence, duplicateAction);
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x00097170 File Offset: 0x00095370
		[SecurityCritical]
		private void QueryHostForPossibleEvidenceTypes()
		{
			if (AppDomain.CurrentDomain.DomainManager != null)
			{
				HostSecurityManager hostSecurityManager = AppDomain.CurrentDomain.DomainManager.HostSecurityManager;
				if (hostSecurityManager != null)
				{
					Type[] array = null;
					AppDomain appDomain = this.m_target.Target as AppDomain;
					Assembly assembly = this.m_target.Target as Assembly;
					if (assembly != null && (hostSecurityManager.Flags & HostSecurityManagerOptions.HostAssemblyEvidence) == HostSecurityManagerOptions.HostAssemblyEvidence)
					{
						array = hostSecurityManager.GetHostSuppliedAssemblyEvidenceTypes(assembly);
					}
					else if (appDomain != null && (hostSecurityManager.Flags & HostSecurityManagerOptions.HostAppDomainEvidence) == HostSecurityManagerOptions.HostAppDomainEvidence)
					{
						array = hostSecurityManager.GetHostSuppliedAppDomainEvidenceTypes();
					}
					if (array != null)
					{
						foreach (Type evidenceType in array)
						{
							EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(evidenceType, true);
							evidenceTypeDescriptor.HostCanGenerate = true;
						}
					}
				}
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060028F0 RID: 10480 RVA: 0x0009722C File Offset: 0x0009542C
		internal bool IsUnmodified
		{
			get
			{
				return this.m_version == 0U;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x00097237 File Offset: 0x00095437
		// (set) Token: 0x060028F2 RID: 10482 RVA: 0x0009723F File Offset: 0x0009543F
		public bool Locked
		{
			get
			{
				return this.m_locked;
			}
			[SecuritySafeCritical]
			set
			{
				if (!value)
				{
					new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
					this.m_locked = false;
					return;
				}
				this.m_locked = true;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060028F3 RID: 10483 RVA: 0x0009725F File Offset: 0x0009545F
		// (set) Token: 0x060028F4 RID: 10484 RVA: 0x00097268 File Offset: 0x00095468
		internal IRuntimeEvidenceFactory Target
		{
			get
			{
				return this.m_target;
			}
			[SecurityCritical]
			set
			{
				using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
				{
					this.m_target = value;
					this.QueryHostForPossibleEvidenceTypes();
				}
			}
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x000972A8 File Offset: 0x000954A8
		private static Type GetEvidenceIndexType(EvidenceBase evidence)
		{
			ILegacyEvidenceAdapter legacyEvidenceAdapter = evidence as ILegacyEvidenceAdapter;
			if (legacyEvidenceAdapter != null)
			{
				return legacyEvidenceAdapter.EvidenceType;
			}
			return evidence.GetType();
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000972CC File Offset: 0x000954CC
		internal EvidenceTypeDescriptor GetEvidenceTypeDescriptor(Type evidenceType)
		{
			return this.GetEvidenceTypeDescriptor(evidenceType, false);
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000972D8 File Offset: 0x000954D8
		private EvidenceTypeDescriptor GetEvidenceTypeDescriptor(Type evidenceType, bool addIfNotExist)
		{
			EvidenceTypeDescriptor evidenceTypeDescriptor = null;
			if (!this.m_evidence.TryGetValue(evidenceType, out evidenceTypeDescriptor) && !addIfNotExist)
			{
				return null;
			}
			if (evidenceTypeDescriptor == null)
			{
				evidenceTypeDescriptor = new EvidenceTypeDescriptor();
				bool flag = false;
				LockCookie lockCookie = default(LockCookie);
				try
				{
					if (!this.IsWriterLockHeld)
					{
						lockCookie = this.UpgradeToWriterLock();
						flag = true;
					}
					this.m_evidence[evidenceType] = evidenceTypeDescriptor;
				}
				finally
				{
					if (flag)
					{
						this.DowngradeFromWriterLock(ref lockCookie);
					}
				}
			}
			return evidenceTypeDescriptor;
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x0009734C File Offset: 0x0009554C
		private static EvidenceBase HandleDuplicateEvidence(EvidenceBase original, EvidenceBase duplicate, Evidence.DuplicateEvidenceAction action)
		{
			switch (action)
			{
			case Evidence.DuplicateEvidenceAction.Throw:
				throw new InvalidOperationException(Environment.GetResourceString("Policy_DuplicateEvidence", new object[]
				{
					duplicate.GetType().FullName
				}));
			case Evidence.DuplicateEvidenceAction.Merge:
			{
				LegacyEvidenceList legacyEvidenceList = original as LegacyEvidenceList;
				if (legacyEvidenceList == null)
				{
					legacyEvidenceList = new LegacyEvidenceList();
					legacyEvidenceList.Add(original);
				}
				legacyEvidenceList.Add(duplicate);
				return legacyEvidenceList;
			}
			case Evidence.DuplicateEvidenceAction.SelectNewObject:
				return duplicate;
			default:
				return null;
			}
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000973B4 File Offset: 0x000955B4
		private static EvidenceBase WrapLegacyEvidence(object evidence)
		{
			EvidenceBase evidenceBase = evidence as EvidenceBase;
			if (evidenceBase == null)
			{
				evidenceBase = new LegacyEvidenceWrapper(evidence);
			}
			return evidenceBase;
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x000973D4 File Offset: 0x000955D4
		private static object UnwrapEvidence(EvidenceBase evidence)
		{
			ILegacyEvidenceAdapter legacyEvidenceAdapter = evidence as ILegacyEvidenceAdapter;
			if (legacyEvidenceAdapter != null)
			{
				return legacyEvidenceAdapter.EvidenceObject;
			}
			return evidence;
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x000973F4 File Offset: 0x000955F4
		[SecuritySafeCritical]
		public void Merge(Evidence evidence)
		{
			if (evidence == null)
			{
				return;
			}
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
			{
				bool flag = false;
				IEnumerator hostEnumerator = evidence.GetHostEnumerator();
				while (hostEnumerator.MoveNext())
				{
					if (this.Locked && !flag)
					{
						new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
						flag = true;
					}
					Type type = hostEnumerator.Current.GetType();
					if (this.m_evidence.ContainsKey(type))
					{
						this.GetHostEvidenceNoLock(type);
					}
					EvidenceBase evidence2 = Evidence.WrapLegacyEvidence(hostEnumerator.Current);
					this.AddHostEvidenceNoLock(evidence2, Evidence.GetEvidenceIndexType(evidence2), Evidence.DuplicateEvidenceAction.Merge);
				}
				IEnumerator assemblyEnumerator = evidence.GetAssemblyEnumerator();
				while (assemblyEnumerator.MoveNext())
				{
					object evidence3 = assemblyEnumerator.Current;
					EvidenceBase evidence4 = Evidence.WrapLegacyEvidence(evidence3);
					this.AddAssemblyEvidenceNoLock(evidence4, Evidence.GetEvidenceIndexType(evidence4), Evidence.DuplicateEvidenceAction.Merge);
				}
			}
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x000974C8 File Offset: 0x000956C8
		internal void MergeWithNoDuplicates(Evidence evidence)
		{
			if (evidence == null)
			{
				return;
			}
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
			{
				IEnumerator hostEnumerator = evidence.GetHostEnumerator();
				while (hostEnumerator.MoveNext())
				{
					object evidence2 = hostEnumerator.Current;
					EvidenceBase evidence3 = Evidence.WrapLegacyEvidence(evidence2);
					this.AddHostEvidenceNoLock(evidence3, Evidence.GetEvidenceIndexType(evidence3), Evidence.DuplicateEvidenceAction.SelectNewObject);
				}
				IEnumerator assemblyEnumerator = evidence.GetAssemblyEnumerator();
				while (assemblyEnumerator.MoveNext())
				{
					object evidence4 = assemblyEnumerator.Current;
					EvidenceBase evidence5 = Evidence.WrapLegacyEvidence(evidence4);
					this.AddAssemblyEvidenceNoLock(evidence5, Evidence.GetEvidenceIndexType(evidence5), Evidence.DuplicateEvidenceAction.SelectNewObject);
				}
			}
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x00097558 File Offset: 0x00095758
		[ComVisible(false)]
		[OnSerializing]
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private void OnSerializing(StreamingContext context)
		{
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
			{
				foreach (Type type in new List<Type>(this.m_evidence.Keys))
				{
					this.GetHostEvidenceNoLock(type);
				}
				this.DeserializeTargetEvidence();
			}
			ArrayList arrayList = new ArrayList();
			IEnumerator hostEnumerator = this.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object value = hostEnumerator.Current;
				arrayList.Add(value);
			}
			this.m_hostList = arrayList;
			ArrayList arrayList2 = new ArrayList();
			IEnumerator assemblyEnumerator = this.GetAssemblyEnumerator();
			while (assemblyEnumerator.MoveNext())
			{
				object value2 = assemblyEnumerator.Current;
				arrayList2.Add(value2);
			}
			this.m_assemblyList = arrayList2;
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x0009763C File Offset: 0x0009583C
		[ComVisible(false)]
		[OnDeserialized]
		[SecurityCritical]
		private void OnDeserialized(StreamingContext context)
		{
			if (this.m_evidence == null)
			{
				this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
				if (this.m_hostList != null)
				{
					foreach (object obj in this.m_hostList)
					{
						if (obj != null)
						{
							this.AddHost(obj);
						}
					}
					this.m_hostList = null;
				}
				if (this.m_assemblyList != null)
				{
					foreach (object obj2 in this.m_assemblyList)
					{
						if (obj2 != null)
						{
							this.AddAssembly(obj2);
						}
					}
					this.m_assemblyList = null;
				}
			}
			this.m_evidenceLock = new ReaderWriterLock();
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x00097728 File Offset: 0x00095928
		private void DeserializeTargetEvidence()
		{
			if (this.m_target != null && !this.m_deserializedTargetEvidence)
			{
				bool flag = false;
				LockCookie lockCookie = default(LockCookie);
				try
				{
					if (!this.IsWriterLockHeld)
					{
						lockCookie = this.UpgradeToWriterLock();
						flag = true;
					}
					this.m_deserializedTargetEvidence = true;
					foreach (EvidenceBase evidence in this.m_target.GetFactorySuppliedEvidence())
					{
						this.AddAssemblyEvidenceNoLock(evidence, Evidence.GetEvidenceIndexType(evidence), Evidence.DuplicateEvidenceAction.Throw);
					}
				}
				finally
				{
					if (flag)
					{
						this.DowngradeFromWriterLock(ref lockCookie);
					}
				}
			}
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x000977CC File Offset: 0x000959CC
		[SecurityCritical]
		internal byte[] RawSerialize()
		{
			byte[] result;
			try
			{
				using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
				{
					Dictionary<Type, EvidenceBase> dictionary = new Dictionary<Type, EvidenceBase>();
					foreach (KeyValuePair<Type, EvidenceTypeDescriptor> keyValuePair in this.m_evidence)
					{
						if (keyValuePair.Value != null && keyValuePair.Value.HostEvidence != null)
						{
							dictionary[keyValuePair.Key] = keyValuePair.Value.HostEvidence;
						}
					}
					using (MemoryStream memoryStream = new MemoryStream())
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						binaryFormatter.Serialize(memoryStream, dictionary);
						result = memoryStream.ToArray();
					}
				}
			}
			catch (SecurityException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x000978BC File Offset: 0x00095ABC
		[Obsolete("Evidence should not be treated as an ICollection. Please use the GetHostEnumerator and GetAssemblyEnumerator methods rather than using CopyTo.")]
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index > array.Length - this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			int num = index;
			IEnumerator hostEnumerator = this.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object value = hostEnumerator.Current;
				array.SetValue(value, num);
				num++;
			}
			IEnumerator assemblyEnumerator = this.GetAssemblyEnumerator();
			while (assemblyEnumerator.MoveNext())
			{
				object value2 = assemblyEnumerator.Current;
				array.SetValue(value2, num);
				num++;
			}
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x0009793C File Offset: 0x00095B3C
		public IEnumerator GetHostEnumerator()
		{
			IEnumerator result;
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
			{
				result = new Evidence.EvidenceEnumerator(this, Evidence.EvidenceEnumerator.Category.Host);
			}
			return result;
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x00097978 File Offset: 0x00095B78
		public IEnumerator GetAssemblyEnumerator()
		{
			IEnumerator result;
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
			{
				this.DeserializeTargetEvidence();
				result = new Evidence.EvidenceEnumerator(this, Evidence.EvidenceEnumerator.Category.Assembly);
			}
			return result;
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x000979B8 File Offset: 0x00095BB8
		internal Evidence.RawEvidenceEnumerator GetRawAssemblyEvidenceEnumerator()
		{
			this.DeserializeTargetEvidence();
			return new Evidence.RawEvidenceEnumerator(this, new List<Type>(this.m_evidence.Keys), false);
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x000979D7 File Offset: 0x00095BD7
		internal Evidence.RawEvidenceEnumerator GetRawHostEvidenceEnumerator()
		{
			return new Evidence.RawEvidenceEnumerator(this, new List<Type>(this.m_evidence.Keys), true);
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x000979F0 File Offset: 0x00095BF0
		[Obsolete("GetEnumerator is obsolete. Please use GetAssemblyEnumerator and GetHostEnumerator instead.")]
		public IEnumerator GetEnumerator()
		{
			IEnumerator result;
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
			{
				result = new Evidence.EvidenceEnumerator(this, Evidence.EvidenceEnumerator.Category.Host | Evidence.EvidenceEnumerator.Category.Assembly);
			}
			return result;
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x00097A2C File Offset: 0x00095C2C
		[ComVisible(false)]
		public T GetAssemblyEvidence<T>() where T : EvidenceBase
		{
			return Evidence.UnwrapEvidence(this.GetAssemblyEvidence(typeof(T))) as T;
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x00097A50 File Offset: 0x00095C50
		internal EvidenceBase GetAssemblyEvidence(Type type)
		{
			EvidenceBase assemblyEvidenceNoLock;
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
			{
				assemblyEvidenceNoLock = this.GetAssemblyEvidenceNoLock(type);
			}
			return assemblyEvidenceNoLock;
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x00097A8C File Offset: 0x00095C8C
		private EvidenceBase GetAssemblyEvidenceNoLock(Type type)
		{
			this.DeserializeTargetEvidence();
			EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(type);
			if (evidenceTypeDescriptor != null)
			{
				return evidenceTypeDescriptor.AssemblyEvidence;
			}
			return null;
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x00097AB2 File Offset: 0x00095CB2
		[ComVisible(false)]
		public T GetHostEvidence<T>() where T : EvidenceBase
		{
			return Evidence.UnwrapEvidence(this.GetHostEvidence(typeof(T))) as T;
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x00097AD3 File Offset: 0x00095CD3
		internal T GetDelayEvaluatedHostEvidence<T>() where T : EvidenceBase, IDelayEvaluatedEvidence
		{
			return Evidence.UnwrapEvidence(this.GetHostEvidence(typeof(T), false)) as T;
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x00097AF5 File Offset: 0x00095CF5
		internal EvidenceBase GetHostEvidence(Type type)
		{
			return this.GetHostEvidence(type, true);
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x00097B00 File Offset: 0x00095D00
		[SecuritySafeCritical]
		private EvidenceBase GetHostEvidence(Type type, bool markDelayEvaluatedEvidenceUsed)
		{
			EvidenceBase result;
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
			{
				EvidenceBase hostEvidenceNoLock = this.GetHostEvidenceNoLock(type);
				if (markDelayEvaluatedEvidenceUsed)
				{
					IDelayEvaluatedEvidence delayEvaluatedEvidence = hostEvidenceNoLock as IDelayEvaluatedEvidence;
					if (delayEvaluatedEvidence != null)
					{
						delayEvaluatedEvidence.MarkUsed();
					}
				}
				result = hostEvidenceNoLock;
			}
			return result;
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x00097B50 File Offset: 0x00095D50
		[SecurityCritical]
		private EvidenceBase GetHostEvidenceNoLock(Type type)
		{
			EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(type);
			if (evidenceTypeDescriptor == null)
			{
				return null;
			}
			if (evidenceTypeDescriptor.HostEvidence != null)
			{
				return evidenceTypeDescriptor.HostEvidence;
			}
			if (this.m_target != null && !evidenceTypeDescriptor.Generated)
			{
				using (new Evidence.EvidenceUpgradeLockHolder(this))
				{
					evidenceTypeDescriptor.Generated = true;
					EvidenceBase evidenceBase = this.GenerateHostEvidence(type, evidenceTypeDescriptor.HostCanGenerate);
					if (evidenceBase != null)
					{
						evidenceTypeDescriptor.HostEvidence = evidenceBase;
						Evidence evidence = (this.m_cloneOrigin != null) ? (this.m_cloneOrigin.Target as Evidence) : null;
						if (evidence != null)
						{
							using (new Evidence.EvidenceLockHolder(evidence, Evidence.EvidenceLockHolder.LockType.Writer))
							{
								EvidenceTypeDescriptor evidenceTypeDescriptor2 = evidence.GetEvidenceTypeDescriptor(type);
								if (evidenceTypeDescriptor2 != null && evidenceTypeDescriptor2.HostEvidence == null)
								{
									evidenceTypeDescriptor2.HostEvidence = evidenceBase.Clone();
								}
							}
						}
					}
					return evidenceBase;
				}
			}
			return null;
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x00097C40 File Offset: 0x00095E40
		[SecurityCritical]
		private EvidenceBase GenerateHostEvidence(Type type, bool hostCanGenerate)
		{
			if (hostCanGenerate)
			{
				AppDomain appDomain = this.m_target.Target as AppDomain;
				Assembly assembly = this.m_target.Target as Assembly;
				EvidenceBase evidenceBase = null;
				if (appDomain != null)
				{
					evidenceBase = AppDomain.CurrentDomain.HostSecurityManager.GenerateAppDomainEvidence(type);
				}
				else if (assembly != null)
				{
					evidenceBase = AppDomain.CurrentDomain.HostSecurityManager.GenerateAssemblyEvidence(type, assembly);
				}
				if (evidenceBase != null)
				{
					if (!type.IsAssignableFrom(evidenceBase.GetType()))
					{
						string fullName = AppDomain.CurrentDomain.HostSecurityManager.GetType().FullName;
						string fullName2 = evidenceBase.GetType().FullName;
						string fullName3 = type.FullName;
						throw new InvalidOperationException(Environment.GetResourceString("Policy_IncorrectHostEvidence", new object[]
						{
							fullName,
							fullName2,
							fullName3
						}));
					}
					return evidenceBase;
				}
			}
			return this.m_target.GenerateEvidence(type);
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06002910 RID: 10512 RVA: 0x00097D18 File Offset: 0x00095F18
		[Obsolete("Evidence should not be treated as an ICollection. Please use GetHostEnumerator and GetAssemblyEnumerator to iterate over the evidence to collect a count.")]
		public int Count
		{
			get
			{
				int num = 0;
				IEnumerator hostEnumerator = this.GetHostEnumerator();
				while (hostEnumerator.MoveNext())
				{
					num++;
				}
				IEnumerator assemblyEnumerator = this.GetAssemblyEnumerator();
				while (assemblyEnumerator.MoveNext())
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06002911 RID: 10513 RVA: 0x00097D54 File Offset: 0x00095F54
		[ComVisible(false)]
		internal int RawCount
		{
			get
			{
				int num = 0;
				using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
				{
					foreach (Type evidenceType in new List<Type>(this.m_evidence.Keys))
					{
						EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(evidenceType);
						if (evidenceTypeDescriptor != null)
						{
							if (evidenceTypeDescriptor.AssemblyEvidence != null)
							{
								num++;
							}
							if (evidenceTypeDescriptor.HostEvidence != null)
							{
								num++;
							}
						}
					}
				}
				return num;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06002912 RID: 10514 RVA: 0x00097DF4 File Offset: 0x00095FF4
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06002913 RID: 10515 RVA: 0x00097DF7 File Offset: 0x00095FF7
		public bool IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06002914 RID: 10516 RVA: 0x00097DFA File Offset: 0x00095FFA
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x00097DFD File Offset: 0x00095FFD
		[ComVisible(false)]
		public Evidence Clone()
		{
			return new Evidence(this);
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x00097E08 File Offset: 0x00096008
		[ComVisible(false)]
		[SecuritySafeCritical]
		public void Clear()
		{
			if (this.Locked)
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
			}
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
			{
				this.m_version += 1U;
				this.m_evidence.Clear();
			}
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x00097E68 File Offset: 0x00096068
		[ComVisible(false)]
		[SecuritySafeCritical]
		public void RemoveType(Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
			{
				EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(t);
				if (evidenceTypeDescriptor != null)
				{
					this.m_version += 1U;
					if (this.Locked && (evidenceTypeDescriptor.HostEvidence != null || evidenceTypeDescriptor.HostCanGenerate))
					{
						new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
					}
					this.m_evidence.Remove(t);
				}
			}
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x00097EF8 File Offset: 0x000960F8
		internal void MarkAllEvidenceAsUsed()
		{
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
			{
				foreach (KeyValuePair<Type, EvidenceTypeDescriptor> keyValuePair in this.m_evidence)
				{
					if (keyValuePair.Value != null)
					{
						IDelayEvaluatedEvidence delayEvaluatedEvidence = keyValuePair.Value.HostEvidence as IDelayEvaluatedEvidence;
						if (delayEvaluatedEvidence != null)
						{
							delayEvaluatedEvidence.MarkUsed();
						}
						IDelayEvaluatedEvidence delayEvaluatedEvidence2 = keyValuePair.Value.AssemblyEvidence as IDelayEvaluatedEvidence;
						if (delayEvaluatedEvidence2 != null)
						{
							delayEvaluatedEvidence2.MarkUsed();
						}
					}
				}
			}
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x00097FA4 File Offset: 0x000961A4
		private bool WasStrongNameEvidenceUsed()
		{
			bool result;
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
			{
				EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(typeof(StrongName));
				if (evidenceTypeDescriptor != null)
				{
					IDelayEvaluatedEvidence delayEvaluatedEvidence = evidenceTypeDescriptor.HostEvidence as IDelayEvaluatedEvidence;
					result = (delayEvaluatedEvidence != null && delayEvaluatedEvidence.WasUsed);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x04001076 RID: 4214
		[OptionalField(VersionAdded = 4)]
		private Dictionary<Type, EvidenceTypeDescriptor> m_evidence;

		// Token: 0x04001077 RID: 4215
		[OptionalField(VersionAdded = 4)]
		private bool m_deserializedTargetEvidence;

		// Token: 0x04001078 RID: 4216
		private volatile ArrayList m_hostList;

		// Token: 0x04001079 RID: 4217
		private volatile ArrayList m_assemblyList;

		// Token: 0x0400107A RID: 4218
		[NonSerialized]
		private ReaderWriterLock m_evidenceLock;

		// Token: 0x0400107B RID: 4219
		[NonSerialized]
		private uint m_version;

		// Token: 0x0400107C RID: 4220
		[NonSerialized]
		private IRuntimeEvidenceFactory m_target;

		// Token: 0x0400107D RID: 4221
		private bool m_locked;

		// Token: 0x0400107E RID: 4222
		[NonSerialized]
		private WeakReference m_cloneOrigin;

		// Token: 0x0400107F RID: 4223
		private static volatile Type[] s_runtimeEvidenceTypes;

		// Token: 0x04001080 RID: 4224
		private const int LockTimeout = 5000;

		// Token: 0x02000B22 RID: 2850
		private enum DuplicateEvidenceAction
		{
			// Token: 0x0400332F RID: 13103
			Throw,
			// Token: 0x04003330 RID: 13104
			Merge,
			// Token: 0x04003331 RID: 13105
			SelectNewObject
		}

		// Token: 0x02000B23 RID: 2851
		private class EvidenceLockHolder : IDisposable
		{
			// Token: 0x06006AB8 RID: 27320 RVA: 0x00170993 File Offset: 0x0016EB93
			public EvidenceLockHolder(Evidence target, Evidence.EvidenceLockHolder.LockType lockType)
			{
				this.m_target = target;
				this.m_lockType = lockType;
				if (this.m_lockType == Evidence.EvidenceLockHolder.LockType.Reader)
				{
					this.m_target.AcquireReaderLock();
					return;
				}
				this.m_target.AcquireWriterlock();
			}

			// Token: 0x06006AB9 RID: 27321 RVA: 0x001709C8 File Offset: 0x0016EBC8
			public void Dispose()
			{
				if (this.m_lockType == Evidence.EvidenceLockHolder.LockType.Reader && this.m_target.IsReaderLockHeld)
				{
					this.m_target.ReleaseReaderLock();
					return;
				}
				if (this.m_lockType == Evidence.EvidenceLockHolder.LockType.Writer && this.m_target.IsWriterLockHeld)
				{
					this.m_target.ReleaseWriterLock();
				}
			}

			// Token: 0x04003332 RID: 13106
			private Evidence m_target;

			// Token: 0x04003333 RID: 13107
			private Evidence.EvidenceLockHolder.LockType m_lockType;

			// Token: 0x02000CC9 RID: 3273
			public enum LockType
			{
				// Token: 0x04003847 RID: 14407
				Reader,
				// Token: 0x04003848 RID: 14408
				Writer
			}
		}

		// Token: 0x02000B24 RID: 2852
		private class EvidenceUpgradeLockHolder : IDisposable
		{
			// Token: 0x06006ABA RID: 27322 RVA: 0x00170A17 File Offset: 0x0016EC17
			public EvidenceUpgradeLockHolder(Evidence target)
			{
				this.m_target = target;
				this.m_cookie = this.m_target.UpgradeToWriterLock();
			}

			// Token: 0x06006ABB RID: 27323 RVA: 0x00170A37 File Offset: 0x0016EC37
			public void Dispose()
			{
				if (this.m_target.IsWriterLockHeld)
				{
					this.m_target.DowngradeFromWriterLock(ref this.m_cookie);
				}
			}

			// Token: 0x04003334 RID: 13108
			private Evidence m_target;

			// Token: 0x04003335 RID: 13109
			private LockCookie m_cookie;
		}

		// Token: 0x02000B25 RID: 2853
		internal sealed class RawEvidenceEnumerator : IEnumerator<EvidenceBase>, IDisposable, IEnumerator
		{
			// Token: 0x06006ABC RID: 27324 RVA: 0x00170A57 File Offset: 0x0016EC57
			public RawEvidenceEnumerator(Evidence evidence, IEnumerable<Type> evidenceTypes, bool hostEnumerator)
			{
				this.m_evidence = evidence;
				this.m_hostEnumerator = hostEnumerator;
				this.m_evidenceTypes = Evidence.RawEvidenceEnumerator.GenerateEvidenceTypes(evidence, evidenceTypes, hostEnumerator);
				this.m_evidenceVersion = evidence.m_version;
				this.Reset();
			}

			// Token: 0x1700121A RID: 4634
			// (get) Token: 0x06006ABD RID: 27325 RVA: 0x00170A8D File Offset: 0x0016EC8D
			public EvidenceBase Current
			{
				get
				{
					if (this.m_evidence.m_version != this.m_evidenceVersion)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					return this.m_currentEvidence;
				}
			}

			// Token: 0x1700121B RID: 4635
			// (get) Token: 0x06006ABE RID: 27326 RVA: 0x00170AB8 File Offset: 0x0016ECB8
			object IEnumerator.Current
			{
				get
				{
					if (this.m_evidence.m_version != this.m_evidenceVersion)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					return this.m_currentEvidence;
				}
			}

			// Token: 0x1700121C RID: 4636
			// (get) Token: 0x06006ABF RID: 27327 RVA: 0x00170AE4 File Offset: 0x0016ECE4
			private static List<Type> ExpensiveEvidence
			{
				get
				{
					if (Evidence.RawEvidenceEnumerator.s_expensiveEvidence == null)
					{
						Evidence.RawEvidenceEnumerator.s_expensiveEvidence = new List<Type>
						{
							typeof(Hash),
							typeof(Publisher)
						};
					}
					return Evidence.RawEvidenceEnumerator.s_expensiveEvidence;
				}
			}

			// Token: 0x06006AC0 RID: 27328 RVA: 0x00170B2F File Offset: 0x0016ED2F
			public void Dispose()
			{
			}

			// Token: 0x06006AC1 RID: 27329 RVA: 0x00170B34 File Offset: 0x0016ED34
			private static Type[] GenerateEvidenceTypes(Evidence evidence, IEnumerable<Type> evidenceTypes, bool hostEvidence)
			{
				List<Type> list = new List<Type>();
				List<Type> list2 = new List<Type>();
				List<Type> list3 = new List<Type>(Evidence.RawEvidenceEnumerator.ExpensiveEvidence.Count);
				foreach (Type type in evidenceTypes)
				{
					EvidenceTypeDescriptor evidenceTypeDescriptor = evidence.GetEvidenceTypeDescriptor(type);
					bool flag = (hostEvidence && evidenceTypeDescriptor.HostEvidence != null) || (!hostEvidence && evidenceTypeDescriptor.AssemblyEvidence != null);
					if (flag)
					{
						list.Add(type);
					}
					else if (Evidence.RawEvidenceEnumerator.ExpensiveEvidence.Contains(type))
					{
						list3.Add(type);
					}
					else
					{
						list2.Add(type);
					}
				}
				Type[] array = new Type[list.Count + list2.Count + list3.Count];
				list.CopyTo(array, 0);
				list2.CopyTo(array, list.Count);
				list3.CopyTo(array, list.Count + list2.Count);
				return array;
			}

			// Token: 0x06006AC2 RID: 27330 RVA: 0x00170C34 File Offset: 0x0016EE34
			[SecuritySafeCritical]
			public bool MoveNext()
			{
				using (new Evidence.EvidenceLockHolder(this.m_evidence, Evidence.EvidenceLockHolder.LockType.Reader))
				{
					if (this.m_evidence.m_version != this.m_evidenceVersion)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					this.m_currentEvidence = null;
					do
					{
						this.m_typeIndex++;
						if (this.m_typeIndex < this.m_evidenceTypes.Length)
						{
							if (this.m_hostEnumerator)
							{
								this.m_currentEvidence = this.m_evidence.GetHostEvidenceNoLock(this.m_evidenceTypes[this.m_typeIndex]);
							}
							else
							{
								this.m_currentEvidence = this.m_evidence.GetAssemblyEvidenceNoLock(this.m_evidenceTypes[this.m_typeIndex]);
							}
						}
					}
					while (this.m_typeIndex < this.m_evidenceTypes.Length && this.m_currentEvidence == null);
				}
				return this.m_currentEvidence != null;
			}

			// Token: 0x06006AC3 RID: 27331 RVA: 0x00170D1C File Offset: 0x0016EF1C
			public void Reset()
			{
				if (this.m_evidence.m_version != this.m_evidenceVersion)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this.m_typeIndex = -1;
				this.m_currentEvidence = null;
			}

			// Token: 0x04003336 RID: 13110
			private Evidence m_evidence;

			// Token: 0x04003337 RID: 13111
			private bool m_hostEnumerator;

			// Token: 0x04003338 RID: 13112
			private uint m_evidenceVersion;

			// Token: 0x04003339 RID: 13113
			private Type[] m_evidenceTypes;

			// Token: 0x0400333A RID: 13114
			private int m_typeIndex;

			// Token: 0x0400333B RID: 13115
			private EvidenceBase m_currentEvidence;

			// Token: 0x0400333C RID: 13116
			private static volatile List<Type> s_expensiveEvidence;
		}

		// Token: 0x02000B26 RID: 2854
		private sealed class EvidenceEnumerator : IEnumerator
		{
			// Token: 0x06006AC4 RID: 27332 RVA: 0x00170D4F File Offset: 0x0016EF4F
			internal EvidenceEnumerator(Evidence evidence, Evidence.EvidenceEnumerator.Category category)
			{
				this.m_evidence = evidence;
				this.m_category = category;
				this.ResetNoLock();
			}

			// Token: 0x06006AC5 RID: 27333 RVA: 0x00170D6C File Offset: 0x0016EF6C
			public bool MoveNext()
			{
				IEnumerator currentEnumerator = this.CurrentEnumerator;
				if (currentEnumerator == null)
				{
					this.m_currentEvidence = null;
					return false;
				}
				if (currentEnumerator.MoveNext())
				{
					LegacyEvidenceWrapper legacyEvidenceWrapper = currentEnumerator.Current as LegacyEvidenceWrapper;
					LegacyEvidenceList legacyEvidenceList = currentEnumerator.Current as LegacyEvidenceList;
					if (legacyEvidenceWrapper != null)
					{
						this.m_currentEvidence = legacyEvidenceWrapper.EvidenceObject;
					}
					else if (legacyEvidenceList != null)
					{
						IEnumerator enumerator = legacyEvidenceList.GetEnumerator();
						this.m_enumerators.Push(enumerator);
						this.MoveNext();
					}
					else
					{
						this.m_currentEvidence = currentEnumerator.Current;
					}
					return true;
				}
				this.m_enumerators.Pop();
				return this.MoveNext();
			}

			// Token: 0x1700121D RID: 4637
			// (get) Token: 0x06006AC6 RID: 27334 RVA: 0x00170DFC File Offset: 0x0016EFFC
			public object Current
			{
				get
				{
					return this.m_currentEvidence;
				}
			}

			// Token: 0x1700121E RID: 4638
			// (get) Token: 0x06006AC7 RID: 27335 RVA: 0x00170E04 File Offset: 0x0016F004
			private IEnumerator CurrentEnumerator
			{
				get
				{
					if (this.m_enumerators.Count <= 0)
					{
						return null;
					}
					return this.m_enumerators.Peek() as IEnumerator;
				}
			}

			// Token: 0x06006AC8 RID: 27336 RVA: 0x00170E28 File Offset: 0x0016F028
			public void Reset()
			{
				using (new Evidence.EvidenceLockHolder(this.m_evidence, Evidence.EvidenceLockHolder.LockType.Reader))
				{
					this.ResetNoLock();
				}
			}

			// Token: 0x06006AC9 RID: 27337 RVA: 0x00170E64 File Offset: 0x0016F064
			private void ResetNoLock()
			{
				this.m_currentEvidence = null;
				this.m_enumerators = new Stack();
				if ((this.m_category & Evidence.EvidenceEnumerator.Category.Host) == Evidence.EvidenceEnumerator.Category.Host)
				{
					this.m_enumerators.Push(this.m_evidence.GetRawHostEvidenceEnumerator());
				}
				if ((this.m_category & Evidence.EvidenceEnumerator.Category.Assembly) == Evidence.EvidenceEnumerator.Category.Assembly)
				{
					this.m_enumerators.Push(this.m_evidence.GetRawAssemblyEvidenceEnumerator());
				}
			}

			// Token: 0x0400333D RID: 13117
			private Evidence m_evidence;

			// Token: 0x0400333E RID: 13118
			private Evidence.EvidenceEnumerator.Category m_category;

			// Token: 0x0400333F RID: 13119
			private Stack m_enumerators;

			// Token: 0x04003340 RID: 13120
			private object m_currentEvidence;

			// Token: 0x02000CCA RID: 3274
			[Flags]
			internal enum Category
			{
				// Token: 0x0400384A RID: 14410
				Host = 1,
				// Token: 0x0400384B RID: 14411
				Assembly = 2
			}
		}
	}
}
