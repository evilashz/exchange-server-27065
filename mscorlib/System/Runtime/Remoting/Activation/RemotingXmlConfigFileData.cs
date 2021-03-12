using System;
using System.Collections;
using System.Reflection;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000870 RID: 2160
	internal class RemotingXmlConfigFileData
	{
		// Token: 0x06005C15 RID: 23573 RVA: 0x00142014 File Offset: 0x00140214
		internal void AddInteropXmlElementEntry(string xmlElementName, string xmlElementNamespace, string urtTypeName, string urtAssemblyName)
		{
			this.TryToLoadTypeIfApplicable(urtTypeName, urtAssemblyName);
			RemotingXmlConfigFileData.InteropXmlElementEntry value = new RemotingXmlConfigFileData.InteropXmlElementEntry(xmlElementName, xmlElementNamespace, urtTypeName, urtAssemblyName);
			this.InteropXmlElementEntries.Add(value);
		}

		// Token: 0x06005C16 RID: 23574 RVA: 0x00142044 File Offset: 0x00140244
		internal void AddInteropXmlTypeEntry(string xmlTypeName, string xmlTypeNamespace, string urtTypeName, string urtAssemblyName)
		{
			this.TryToLoadTypeIfApplicable(urtTypeName, urtAssemblyName);
			RemotingXmlConfigFileData.InteropXmlTypeEntry value = new RemotingXmlConfigFileData.InteropXmlTypeEntry(xmlTypeName, xmlTypeNamespace, urtTypeName, urtAssemblyName);
			this.InteropXmlTypeEntries.Add(value);
		}

		// Token: 0x06005C17 RID: 23575 RVA: 0x00142074 File Offset: 0x00140274
		internal void AddPreLoadEntry(string typeName, string assemblyName)
		{
			this.TryToLoadTypeIfApplicable(typeName, assemblyName);
			RemotingXmlConfigFileData.PreLoadEntry value = new RemotingXmlConfigFileData.PreLoadEntry(typeName, assemblyName);
			this.PreLoadEntries.Add(value);
		}

		// Token: 0x06005C18 RID: 23576 RVA: 0x001420A0 File Offset: 0x001402A0
		internal RemotingXmlConfigFileData.RemoteAppEntry AddRemoteAppEntry(string appUri)
		{
			RemotingXmlConfigFileData.RemoteAppEntry remoteAppEntry = new RemotingXmlConfigFileData.RemoteAppEntry(appUri);
			this.RemoteAppEntries.Add(remoteAppEntry);
			return remoteAppEntry;
		}

		// Token: 0x06005C19 RID: 23577 RVA: 0x001420C4 File Offset: 0x001402C4
		internal void AddServerActivatedEntry(string typeName, string assemName, ArrayList contextAttributes)
		{
			this.TryToLoadTypeIfApplicable(typeName, assemName);
			RemotingXmlConfigFileData.TypeEntry value = new RemotingXmlConfigFileData.TypeEntry(typeName, assemName, contextAttributes);
			this.ServerActivatedEntries.Add(value);
		}

		// Token: 0x06005C1A RID: 23578 RVA: 0x001420F0 File Offset: 0x001402F0
		internal RemotingXmlConfigFileData.ServerWellKnownEntry AddServerWellKnownEntry(string typeName, string assemName, ArrayList contextAttributes, string objURI, WellKnownObjectMode objMode)
		{
			this.TryToLoadTypeIfApplicable(typeName, assemName);
			RemotingXmlConfigFileData.ServerWellKnownEntry serverWellKnownEntry = new RemotingXmlConfigFileData.ServerWellKnownEntry(typeName, assemName, contextAttributes, objURI, objMode);
			this.ServerWellKnownEntries.Add(serverWellKnownEntry);
			return serverWellKnownEntry;
		}

		// Token: 0x06005C1B RID: 23579 RVA: 0x00142120 File Offset: 0x00140320
		private void TryToLoadTypeIfApplicable(string typeName, string assemblyName)
		{
			if (!RemotingXmlConfigFileData.LoadTypes)
			{
				return;
			}
			Assembly assembly = Assembly.Load(assemblyName);
			if (assembly == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_AssemblyLoadFailed", new object[]
				{
					assemblyName
				}));
			}
			Type type = assembly.GetType(typeName, false, false);
			if (type == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_BadType", new object[]
				{
					typeName
				}));
			}
		}

		// Token: 0x04002940 RID: 10560
		internal static volatile bool LoadTypes;

		// Token: 0x04002941 RID: 10561
		internal string ApplicationName;

		// Token: 0x04002942 RID: 10562
		internal RemotingXmlConfigFileData.LifetimeEntry Lifetime;

		// Token: 0x04002943 RID: 10563
		internal bool UrlObjRefMode = RemotingConfigHandler.UrlObjRefMode;

		// Token: 0x04002944 RID: 10564
		internal RemotingXmlConfigFileData.CustomErrorsEntry CustomErrors;

		// Token: 0x04002945 RID: 10565
		internal ArrayList ChannelEntries = new ArrayList();

		// Token: 0x04002946 RID: 10566
		internal ArrayList InteropXmlElementEntries = new ArrayList();

		// Token: 0x04002947 RID: 10567
		internal ArrayList InteropXmlTypeEntries = new ArrayList();

		// Token: 0x04002948 RID: 10568
		internal ArrayList PreLoadEntries = new ArrayList();

		// Token: 0x04002949 RID: 10569
		internal ArrayList RemoteAppEntries = new ArrayList();

		// Token: 0x0400294A RID: 10570
		internal ArrayList ServerActivatedEntries = new ArrayList();

		// Token: 0x0400294B RID: 10571
		internal ArrayList ServerWellKnownEntries = new ArrayList();

		// Token: 0x02000C49 RID: 3145
		internal class ChannelEntry
		{
			// Token: 0x06006FA1 RID: 28577 RVA: 0x00180082 File Offset: 0x0017E282
			internal ChannelEntry(string typeName, string assemblyName, Hashtable properties)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemblyName;
				this.Properties = properties;
			}

			// Token: 0x04003729 RID: 14121
			internal string TypeName;

			// Token: 0x0400372A RID: 14122
			internal string AssemblyName;

			// Token: 0x0400372B RID: 14123
			internal Hashtable Properties;

			// Token: 0x0400372C RID: 14124
			internal bool DelayLoad;

			// Token: 0x0400372D RID: 14125
			internal ArrayList ClientSinkProviders = new ArrayList();

			// Token: 0x0400372E RID: 14126
			internal ArrayList ServerSinkProviders = new ArrayList();
		}

		// Token: 0x02000C4A RID: 3146
		internal class ClientWellKnownEntry
		{
			// Token: 0x06006FA2 RID: 28578 RVA: 0x001800B5 File Offset: 0x0017E2B5
			internal ClientWellKnownEntry(string typeName, string assemName, string url)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemName;
				this.Url = url;
			}

			// Token: 0x0400372F RID: 14127
			internal string TypeName;

			// Token: 0x04003730 RID: 14128
			internal string AssemblyName;

			// Token: 0x04003731 RID: 14129
			internal string Url;
		}

		// Token: 0x02000C4B RID: 3147
		internal class ContextAttributeEntry
		{
			// Token: 0x06006FA3 RID: 28579 RVA: 0x001800D2 File Offset: 0x0017E2D2
			internal ContextAttributeEntry(string typeName, string assemName, Hashtable properties)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemName;
				this.Properties = properties;
			}

			// Token: 0x04003732 RID: 14130
			internal string TypeName;

			// Token: 0x04003733 RID: 14131
			internal string AssemblyName;

			// Token: 0x04003734 RID: 14132
			internal Hashtable Properties;
		}

		// Token: 0x02000C4C RID: 3148
		internal class InteropXmlElementEntry
		{
			// Token: 0x06006FA4 RID: 28580 RVA: 0x001800EF File Offset: 0x0017E2EF
			internal InteropXmlElementEntry(string xmlElementName, string xmlElementNamespace, string urtTypeName, string urtAssemblyName)
			{
				this.XmlElementName = xmlElementName;
				this.XmlElementNamespace = xmlElementNamespace;
				this.UrtTypeName = urtTypeName;
				this.UrtAssemblyName = urtAssemblyName;
			}

			// Token: 0x04003735 RID: 14133
			internal string XmlElementName;

			// Token: 0x04003736 RID: 14134
			internal string XmlElementNamespace;

			// Token: 0x04003737 RID: 14135
			internal string UrtTypeName;

			// Token: 0x04003738 RID: 14136
			internal string UrtAssemblyName;
		}

		// Token: 0x02000C4D RID: 3149
		internal class CustomErrorsEntry
		{
			// Token: 0x06006FA5 RID: 28581 RVA: 0x00180114 File Offset: 0x0017E314
			internal CustomErrorsEntry(CustomErrorsModes mode)
			{
				this.Mode = mode;
			}

			// Token: 0x04003739 RID: 14137
			internal CustomErrorsModes Mode;
		}

		// Token: 0x02000C4E RID: 3150
		internal class InteropXmlTypeEntry
		{
			// Token: 0x06006FA6 RID: 28582 RVA: 0x00180123 File Offset: 0x0017E323
			internal InteropXmlTypeEntry(string xmlTypeName, string xmlTypeNamespace, string urtTypeName, string urtAssemblyName)
			{
				this.XmlTypeName = xmlTypeName;
				this.XmlTypeNamespace = xmlTypeNamespace;
				this.UrtTypeName = urtTypeName;
				this.UrtAssemblyName = urtAssemblyName;
			}

			// Token: 0x0400373A RID: 14138
			internal string XmlTypeName;

			// Token: 0x0400373B RID: 14139
			internal string XmlTypeNamespace;

			// Token: 0x0400373C RID: 14140
			internal string UrtTypeName;

			// Token: 0x0400373D RID: 14141
			internal string UrtAssemblyName;
		}

		// Token: 0x02000C4F RID: 3151
		internal class LifetimeEntry
		{
			// Token: 0x17001341 RID: 4929
			// (get) Token: 0x06006FA7 RID: 28583 RVA: 0x00180148 File Offset: 0x0017E348
			// (set) Token: 0x06006FA8 RID: 28584 RVA: 0x00180150 File Offset: 0x0017E350
			internal TimeSpan LeaseTime
			{
				get
				{
					return this._leaseTime;
				}
				set
				{
					this._leaseTime = value;
					this.IsLeaseTimeSet = true;
				}
			}

			// Token: 0x17001342 RID: 4930
			// (get) Token: 0x06006FA9 RID: 28585 RVA: 0x00180160 File Offset: 0x0017E360
			// (set) Token: 0x06006FAA RID: 28586 RVA: 0x00180168 File Offset: 0x0017E368
			internal TimeSpan RenewOnCallTime
			{
				get
				{
					return this._renewOnCallTime;
				}
				set
				{
					this._renewOnCallTime = value;
					this.IsRenewOnCallTimeSet = true;
				}
			}

			// Token: 0x17001343 RID: 4931
			// (get) Token: 0x06006FAB RID: 28587 RVA: 0x00180178 File Offset: 0x0017E378
			// (set) Token: 0x06006FAC RID: 28588 RVA: 0x00180180 File Offset: 0x0017E380
			internal TimeSpan SponsorshipTimeout
			{
				get
				{
					return this._sponsorshipTimeout;
				}
				set
				{
					this._sponsorshipTimeout = value;
					this.IsSponsorshipTimeoutSet = true;
				}
			}

			// Token: 0x17001344 RID: 4932
			// (get) Token: 0x06006FAD RID: 28589 RVA: 0x00180190 File Offset: 0x0017E390
			// (set) Token: 0x06006FAE RID: 28590 RVA: 0x00180198 File Offset: 0x0017E398
			internal TimeSpan LeaseManagerPollTime
			{
				get
				{
					return this._leaseManagerPollTime;
				}
				set
				{
					this._leaseManagerPollTime = value;
					this.IsLeaseManagerPollTimeSet = true;
				}
			}

			// Token: 0x0400373E RID: 14142
			internal bool IsLeaseTimeSet;

			// Token: 0x0400373F RID: 14143
			internal bool IsRenewOnCallTimeSet;

			// Token: 0x04003740 RID: 14144
			internal bool IsSponsorshipTimeoutSet;

			// Token: 0x04003741 RID: 14145
			internal bool IsLeaseManagerPollTimeSet;

			// Token: 0x04003742 RID: 14146
			private TimeSpan _leaseTime;

			// Token: 0x04003743 RID: 14147
			private TimeSpan _renewOnCallTime;

			// Token: 0x04003744 RID: 14148
			private TimeSpan _sponsorshipTimeout;

			// Token: 0x04003745 RID: 14149
			private TimeSpan _leaseManagerPollTime;
		}

		// Token: 0x02000C50 RID: 3152
		internal class PreLoadEntry
		{
			// Token: 0x06006FB0 RID: 28592 RVA: 0x001801B0 File Offset: 0x0017E3B0
			public PreLoadEntry(string typeName, string assemblyName)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemblyName;
			}

			// Token: 0x04003746 RID: 14150
			internal string TypeName;

			// Token: 0x04003747 RID: 14151
			internal string AssemblyName;
		}

		// Token: 0x02000C51 RID: 3153
		internal class RemoteAppEntry
		{
			// Token: 0x06006FB1 RID: 28593 RVA: 0x001801C6 File Offset: 0x0017E3C6
			internal RemoteAppEntry(string appUri)
			{
				this.AppUri = appUri;
			}

			// Token: 0x06006FB2 RID: 28594 RVA: 0x001801EC File Offset: 0x0017E3EC
			internal void AddWellKnownEntry(string typeName, string assemName, string url)
			{
				RemotingXmlConfigFileData.ClientWellKnownEntry value = new RemotingXmlConfigFileData.ClientWellKnownEntry(typeName, assemName, url);
				this.WellKnownObjects.Add(value);
			}

			// Token: 0x06006FB3 RID: 28595 RVA: 0x00180210 File Offset: 0x0017E410
			internal void AddActivatedEntry(string typeName, string assemName, ArrayList contextAttributes)
			{
				RemotingXmlConfigFileData.TypeEntry value = new RemotingXmlConfigFileData.TypeEntry(typeName, assemName, contextAttributes);
				this.ActivatedObjects.Add(value);
			}

			// Token: 0x04003748 RID: 14152
			internal string AppUri;

			// Token: 0x04003749 RID: 14153
			internal ArrayList WellKnownObjects = new ArrayList();

			// Token: 0x0400374A RID: 14154
			internal ArrayList ActivatedObjects = new ArrayList();
		}

		// Token: 0x02000C52 RID: 3154
		internal class ServerWellKnownEntry : RemotingXmlConfigFileData.TypeEntry
		{
			// Token: 0x06006FB4 RID: 28596 RVA: 0x00180233 File Offset: 0x0017E433
			internal ServerWellKnownEntry(string typeName, string assemName, ArrayList contextAttributes, string objURI, WellKnownObjectMode objMode) : base(typeName, assemName, contextAttributes)
			{
				this.ObjectURI = objURI;
				this.ObjectMode = objMode;
			}

			// Token: 0x0400374B RID: 14155
			internal string ObjectURI;

			// Token: 0x0400374C RID: 14156
			internal WellKnownObjectMode ObjectMode;
		}

		// Token: 0x02000C53 RID: 3155
		internal class SinkProviderEntry
		{
			// Token: 0x06006FB5 RID: 28597 RVA: 0x0018024E File Offset: 0x0017E44E
			internal SinkProviderEntry(string typeName, string assemName, Hashtable properties, bool isFormatter)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemName;
				this.Properties = properties;
				this.IsFormatter = isFormatter;
			}

			// Token: 0x0400374D RID: 14157
			internal string TypeName;

			// Token: 0x0400374E RID: 14158
			internal string AssemblyName;

			// Token: 0x0400374F RID: 14159
			internal Hashtable Properties;

			// Token: 0x04003750 RID: 14160
			internal ArrayList ProviderData = new ArrayList();

			// Token: 0x04003751 RID: 14161
			internal bool IsFormatter;
		}

		// Token: 0x02000C54 RID: 3156
		internal class TypeEntry
		{
			// Token: 0x06006FB6 RID: 28598 RVA: 0x0018027E File Offset: 0x0017E47E
			internal TypeEntry(string typeName, string assemName, ArrayList contextAttributes)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemName;
				this.ContextAttributes = contextAttributes;
			}

			// Token: 0x04003752 RID: 14162
			internal string TypeName;

			// Token: 0x04003753 RID: 14163
			internal string AssemblyName;

			// Token: 0x04003754 RID: 14164
			internal ArrayList ContextAttributes;
		}
	}
}
