using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200011C RID: 284
	[Serializable]
	public abstract class ServerBasedIdParameter : ADIdParameter
	{
		// Token: 0x06000A1B RID: 2587 RVA: 0x000219B3 File Offset: 0x0001FBB3
		protected ServerBasedIdParameter(string identity) : base(identity)
		{
			this.Initialize(identity);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x000219C3 File Offset: 0x0001FBC3
		protected ServerBasedIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x000219CC File Offset: 0x0001FBCC
		protected ServerBasedIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x000219D5 File Offset: 0x0001FBD5
		protected ServerBasedIdParameter()
		{
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x000219DD File Offset: 0x0001FBDD
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x000219E5 File Offset: 0x0001FBE5
		internal bool AllowLegacy
		{
			get
			{
				return this.allowLegacy;
			}
			set
			{
				this.allowLegacy = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000A21 RID: 2593
		protected abstract ServerRole RoleRestriction { get; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x000219EE File Offset: 0x0001FBEE
		protected string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x000219F6 File Offset: 0x0001FBF6
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x000219FE File Offset: 0x0001FBFE
		protected string CommonName { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00021A07 File Offset: 0x0001FC07
		protected ServerIdParameter ServerId
		{
			get
			{
				return this.serverId;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00021A0F File Offset: 0x0001FC0F
		private static string LocalServerFQDN
		{
			get
			{
				return NativeHelpers.GetLocalComputerFqdn(true);
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00021A18 File Offset: 0x0001FC18
		public override string ToString()
		{
			if (base.InternalADObjectId == null)
			{
				string str = "";
				if (!string.IsNullOrEmpty(this.serverName))
				{
					str = this.serverName + '\\';
				}
				return str + this.CommonName;
			}
			return base.ToString();
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00021A66 File Offset: 0x0001FC66
		internal override void Initialize(ObjectId objectId)
		{
			if (!string.IsNullOrEmpty(this.ServerName))
			{
				throw new InvalidOperationException(Strings.ErrorChangeImmutableType);
			}
			base.Initialize(objectId);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00021A8C File Offset: 0x0001FC8C
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			IEnumerable<T> enumerable = null;
			List<T> list = new List<T>();
			notFoundReason = null;
			int num = 0;
			int num2 = 0;
			if (base.InternalADObjectId != null)
			{
				enumerable = base.GetADObjectIdObjects<T>(base.InternalADObjectId, rootId, subTreeSession, optionalData);
			}
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(enumerable);
			if (wrapper.HasElements())
			{
				using (IEnumerator<T> enumerator = wrapper.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						T item = enumerator.Current;
						if (ServerIdParameter.HasRole((ADObjectId)item.Identity, this.RoleRestriction, (IConfigDataProvider)session) || (this.AllowLegacy && !ServerIdParameter.HasRole((ADObjectId)item.Identity, ServerRole.All, (IConfigDataProvider)session)))
						{
							list.Add(item);
						}
						else if (!ServerIdParameter.HasRole((ADObjectId)item.Identity, ServerRole.All, (IConfigDataProvider)session))
						{
							num2++;
						}
						else
						{
							num++;
						}
					}
					goto IL_21B;
				}
			}
			if (!string.IsNullOrEmpty(this.CommonName) && this.ServerId != null)
			{
				ADObjectId[] matchingIdentities = this.ServerId.GetMatchingIdentities((IConfigDataProvider)session);
				foreach (ADObjectId rootId2 in matchingIdentities)
				{
					enumerable = base.GetObjectsInOrganization<T>(this.CommonName, rootId2, session, optionalData);
					foreach (T item2 in enumerable)
					{
						if (ServerIdParameter.HasRole((ADObjectId)item2.Identity, this.RoleRestriction, (IConfigDataProvider)session) || (this.AllowLegacy && !ServerIdParameter.HasRole((ADObjectId)item2.Identity, ServerRole.All, (IConfigDataProvider)session)))
						{
							list.Add(item2);
						}
						else if (!ServerIdParameter.HasRole((ADObjectId)item2.Identity, ServerRole.All, (IConfigDataProvider)session))
						{
							num2++;
						}
						else
						{
							num++;
						}
					}
				}
			}
			IL_21B:
			if (list.Count == 0)
			{
				if (num2 != 0)
				{
					notFoundReason = new LocalizedString?(Strings.ExceptionLegacyObjects(this.ToString()));
				}
				if (num != 0)
				{
					notFoundReason = new LocalizedString?(Strings.ExceptionRoleNotFoundObjects(this.ToString()));
				}
			}
			return list;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00021D10 File Offset: 0x0001FF10
		protected virtual void Initialize(string identity)
		{
			if (base.InternalADObjectId != null && base.InternalADObjectId.Rdn != null)
			{
				return;
			}
			string[] array = identity.Split(new char[]
			{
				'\\'
			});
			if (array.Length > 2)
			{
				throw new ArgumentException(Strings.ErrorInvalidIdentity(identity), "Identity");
			}
			if (array.Length == 2)
			{
				this.serverName = array[0];
				this.CommonName = array[1];
			}
			else if (array.Length == 1)
			{
				this.serverName = ServerBasedIdParameter.LocalServerFQDN;
				this.CommonName = array[0];
			}
			if (string.IsNullOrEmpty(this.serverName) || string.IsNullOrEmpty(this.CommonName))
			{
				throw new ArgumentException(Strings.ErrorInvalidIdentity(identity), "Identity");
			}
			try
			{
				this.serverId = ServerIdParameter.Parse(this.serverName);
			}
			catch (ArgumentException)
			{
			}
		}

		// Token: 0x0400027A RID: 634
		private string serverName;

		// Token: 0x0400027B RID: 635
		private bool allowLegacy;

		// Token: 0x0400027C RID: 636
		private ServerIdParameter serverId;
	}
}
