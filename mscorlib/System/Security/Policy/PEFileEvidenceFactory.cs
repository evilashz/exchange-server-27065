using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Policy
{
	// Token: 0x02000334 RID: 820
	internal sealed class PEFileEvidenceFactory : IRuntimeEvidenceFactory
	{
		// Token: 0x06002998 RID: 10648 RVA: 0x00099A0A File Offset: 0x00097C0A
		[SecurityCritical]
		private PEFileEvidenceFactory(SafePEFileHandle peFile)
		{
			this.m_peFile = peFile;
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06002999 RID: 10649 RVA: 0x00099A19 File Offset: 0x00097C19
		internal SafePEFileHandle PEFile
		{
			[SecurityCritical]
			get
			{
				return this.m_peFile;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600299A RID: 10650 RVA: 0x00099A21 File Offset: 0x00097C21
		public IEvidenceFactory Target
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x00099A24 File Offset: 0x00097C24
		[SecurityCritical]
		private static Evidence CreateSecurityIdentity(SafePEFileHandle peFile, Evidence hostProvidedEvidence)
		{
			PEFileEvidenceFactory target = new PEFileEvidenceFactory(peFile);
			Evidence evidence = new Evidence(target);
			if (hostProvidedEvidence != null)
			{
				evidence.MergeWithNoDuplicates(hostProvidedEvidence);
			}
			return evidence;
		}

		// Token: 0x0600299C RID: 10652
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void FireEvidenceGeneratedEvent(SafePEFileHandle peFile, EvidenceTypeGenerated type);

		// Token: 0x0600299D RID: 10653 RVA: 0x00099A4A File Offset: 0x00097C4A
		[SecuritySafeCritical]
		internal void FireEvidenceGeneratedEvent(EvidenceTypeGenerated type)
		{
			PEFileEvidenceFactory.FireEvidenceGeneratedEvent(this.m_peFile, type);
		}

		// Token: 0x0600299E RID: 10654
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetAssemblySuppliedEvidence(SafePEFileHandle peFile, ObjectHandleOnStack retSerializedEvidence);

		// Token: 0x0600299F RID: 10655 RVA: 0x00099A58 File Offset: 0x00097C58
		[SecuritySafeCritical]
		public IEnumerable<EvidenceBase> GetFactorySuppliedEvidence()
		{
			if (this.m_assemblyProvidedEvidence == null)
			{
				byte[] array = null;
				PEFileEvidenceFactory.GetAssemblySuppliedEvidence(this.m_peFile, JitHelpers.GetObjectHandleOnStack<byte[]>(ref array));
				this.m_assemblyProvidedEvidence = new List<EvidenceBase>();
				if (array != null)
				{
					Evidence evidence = new Evidence();
					new SecurityPermission(SecurityPermissionFlag.SerializationFormatter).Assert();
					try
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						using (MemoryStream memoryStream = new MemoryStream(array))
						{
							evidence = (Evidence)binaryFormatter.Deserialize(memoryStream);
						}
					}
					catch
					{
					}
					CodeAccessPermission.RevertAssert();
					if (evidence != null)
					{
						IEnumerator assemblyEnumerator = evidence.GetAssemblyEnumerator();
						while (assemblyEnumerator.MoveNext())
						{
							if (assemblyEnumerator.Current != null)
							{
								EvidenceBase evidenceBase = assemblyEnumerator.Current as EvidenceBase;
								if (evidenceBase == null)
								{
									evidenceBase = new LegacyEvidenceWrapper(assemblyEnumerator.Current);
								}
								this.m_assemblyProvidedEvidence.Add(evidenceBase);
							}
						}
					}
				}
			}
			return this.m_assemblyProvidedEvidence;
		}

		// Token: 0x060029A0 RID: 10656
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetLocationEvidence(SafePEFileHandle peFile, out SecurityZone zone, StringHandleOnStack retUrl);

		// Token: 0x060029A1 RID: 10657
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetPublisherCertificate(SafePEFileHandle peFile, ObjectHandleOnStack retCertificate);

		// Token: 0x060029A2 RID: 10658 RVA: 0x00099B48 File Offset: 0x00097D48
		public EvidenceBase GenerateEvidence(Type evidenceType)
		{
			if (evidenceType == typeof(Site))
			{
				return this.GenerateSiteEvidence();
			}
			if (evidenceType == typeof(Url))
			{
				return this.GenerateUrlEvidence();
			}
			if (evidenceType == typeof(Zone))
			{
				return this.GenerateZoneEvidence();
			}
			if (evidenceType == typeof(Publisher))
			{
				return this.GeneratePublisherEvidence();
			}
			return null;
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x00099BBC File Offset: 0x00097DBC
		[SecuritySafeCritical]
		private void GenerateLocationEvidence()
		{
			if (!this.m_generatedLocationEvidence)
			{
				SecurityZone securityZone = SecurityZone.NoZone;
				string text = null;
				PEFileEvidenceFactory.GetLocationEvidence(this.m_peFile, out securityZone, JitHelpers.GetStringHandleOnStack(ref text));
				if (securityZone != SecurityZone.NoZone)
				{
					this.m_zoneEvidence = new Zone(securityZone);
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.m_urlEvidence = new Url(text, true);
					if (!text.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
					{
						this.m_siteEvidence = Site.CreateFromUrl(text);
					}
				}
				this.m_generatedLocationEvidence = true;
			}
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x00099C30 File Offset: 0x00097E30
		[SecuritySafeCritical]
		private Publisher GeneratePublisherEvidence()
		{
			byte[] array = null;
			PEFileEvidenceFactory.GetPublisherCertificate(this.m_peFile, JitHelpers.GetObjectHandleOnStack<byte[]>(ref array));
			if (array == null)
			{
				return null;
			}
			return new Publisher(new X509Certificate(array));
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x00099C61 File Offset: 0x00097E61
		private Site GenerateSiteEvidence()
		{
			if (this.m_siteEvidence == null)
			{
				this.GenerateLocationEvidence();
			}
			return this.m_siteEvidence;
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x00099C77 File Offset: 0x00097E77
		private Url GenerateUrlEvidence()
		{
			if (this.m_urlEvidence == null)
			{
				this.GenerateLocationEvidence();
			}
			return this.m_urlEvidence;
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x00099C8D File Offset: 0x00097E8D
		private Zone GenerateZoneEvidence()
		{
			if (this.m_zoneEvidence == null)
			{
				this.GenerateLocationEvidence();
			}
			return this.m_zoneEvidence;
		}

		// Token: 0x040010AE RID: 4270
		[SecurityCritical]
		private SafePEFileHandle m_peFile;

		// Token: 0x040010AF RID: 4271
		private List<EvidenceBase> m_assemblyProvidedEvidence;

		// Token: 0x040010B0 RID: 4272
		private bool m_generatedLocationEvidence;

		// Token: 0x040010B1 RID: 4273
		private Site m_siteEvidence;

		// Token: 0x040010B2 RID: 4274
		private Url m_urlEvidence;

		// Token: 0x040010B3 RID: 4275
		private Zone m_zoneEvidence;
	}
}
