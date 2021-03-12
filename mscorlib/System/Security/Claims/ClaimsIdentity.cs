using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Security.Claims
{
	// Token: 0x020002F0 RID: 752
	[ComVisible(true)]
	[Serializable]
	public class ClaimsIdentity : IIdentity
	{
		// Token: 0x0600270D RID: 9997 RVA: 0x0008E3A9 File Offset: 0x0008C5A9
		public ClaimsIdentity() : this(null)
		{
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x0008E3B2 File Offset: 0x0008C5B2
		public ClaimsIdentity(IIdentity identity) : this(identity, null)
		{
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x0008E3BC File Offset: 0x0008C5BC
		public ClaimsIdentity(IEnumerable<Claim> claims) : this(null, claims, null, null, null)
		{
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x0008E3C9 File Offset: 0x0008C5C9
		public ClaimsIdentity(string authenticationType) : this(null, null, authenticationType, null, null)
		{
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x0008E3D6 File Offset: 0x0008C5D6
		public ClaimsIdentity(IEnumerable<Claim> claims, string authenticationType) : this(null, claims, authenticationType, null, null)
		{
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x0008E3E3 File Offset: 0x0008C5E3
		public ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims) : this(identity, claims, null, null, null)
		{
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x0008E3F0 File Offset: 0x0008C5F0
		public ClaimsIdentity(string authenticationType, string nameType, string roleType) : this(null, null, authenticationType, nameType, roleType)
		{
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x0008E3FD File Offset: 0x0008C5FD
		public ClaimsIdentity(IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType) : this(null, claims, authenticationType, nameType, roleType)
		{
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x0008E40B File Offset: 0x0008C60B
		public ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType) : this(identity, claims, authenticationType, nameType, roleType, true)
		{
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x0008E41C File Offset: 0x0008C61C
		internal ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType, bool checkAuthType)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			bool flag = false;
			bool flag2 = false;
			if (checkAuthType && identity != null && string.IsNullOrEmpty(authenticationType))
			{
				if (identity is WindowsIdentity)
				{
					try
					{
						this.m_authenticationType = identity.AuthenticationType;
						goto IL_85;
					}
					catch (UnauthorizedAccessException)
					{
						this.m_authenticationType = null;
						goto IL_85;
					}
				}
				this.m_authenticationType = identity.AuthenticationType;
			}
			else
			{
				this.m_authenticationType = authenticationType;
			}
			IL_85:
			if (!string.IsNullOrEmpty(nameType))
			{
				this.m_nameType = nameType;
				flag = true;
			}
			if (!string.IsNullOrEmpty(roleType))
			{
				this.m_roleType = roleType;
				flag2 = true;
			}
			ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
			if (claimsIdentity != null)
			{
				this.m_label = claimsIdentity.m_label;
				if (!flag)
				{
					this.m_nameType = claimsIdentity.m_nameType;
				}
				if (!flag2)
				{
					this.m_roleType = claimsIdentity.m_roleType;
				}
				this.m_bootstrapContext = claimsIdentity.m_bootstrapContext;
				if (claimsIdentity.Actor != null)
				{
					if (this.IsCircular(claimsIdentity.Actor))
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperationException_ActorGraphCircular"));
					}
					if (!AppContextSwitches.SetActorAsReferenceWhenCopyingClaimsIdentity)
					{
						this.m_actor = claimsIdentity.Actor.Clone();
					}
					else
					{
						this.m_actor = claimsIdentity.Actor;
					}
				}
				if (claimsIdentity is WindowsIdentity && !(this is WindowsIdentity))
				{
					this.SafeAddClaims(claimsIdentity.Claims);
				}
				else
				{
					this.SafeAddClaims(claimsIdentity.m_instanceClaims);
				}
				if (claimsIdentity.m_userSerializationData != null)
				{
					this.m_userSerializationData = (claimsIdentity.m_userSerializationData.Clone() as byte[]);
				}
			}
			else if (identity != null && !string.IsNullOrEmpty(identity.Name))
			{
				this.SafeAddClaim(new Claim(this.m_nameType, identity.Name, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", this));
			}
			if (claims != null)
			{
				this.SafeAddClaims(claims);
			}
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x0008E600 File Offset: 0x0008C800
		public ClaimsIdentity(BinaryReader reader)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Initialize(reader);
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x0008E660 File Offset: 0x0008C860
		protected ClaimsIdentity(ClaimsIdentity other)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other.m_actor != null)
			{
				this.m_actor = other.m_actor.Clone();
			}
			this.m_authenticationType = other.m_authenticationType;
			this.m_bootstrapContext = other.m_bootstrapContext;
			this.m_label = other.m_label;
			this.m_nameType = other.m_nameType;
			this.m_roleType = other.m_roleType;
			if (other.m_userSerializationData != null)
			{
				this.m_userSerializationData = (other.m_userSerializationData.Clone() as byte[]);
			}
			this.SafeAddClaims(other.m_instanceClaims);
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x0008E738 File Offset: 0x0008C938
		[SecurityCritical]
		protected ClaimsIdentity(SerializationInfo info, StreamingContext context)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.Deserialize(info, context, true);
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x0008E79C File Offset: 0x0008C99C
		[SecurityCritical]
		protected ClaimsIdentity(SerializationInfo info)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.Deserialize(info, default(StreamingContext), false);
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x0008E805 File Offset: 0x0008CA05
		public virtual string AuthenticationType
		{
			get
			{
				return this.m_authenticationType;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x0008E80D File Offset: 0x0008CA0D
		public virtual bool IsAuthenticated
		{
			get
			{
				return !string.IsNullOrEmpty(this.m_authenticationType);
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x0008E81D File Offset: 0x0008CA1D
		// (set) Token: 0x0600271E RID: 10014 RVA: 0x0008E825 File Offset: 0x0008CA25
		public ClaimsIdentity Actor
		{
			get
			{
				return this.m_actor;
			}
			set
			{
				if (value != null && this.IsCircular(value))
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperationException_ActorGraphCircular"));
				}
				this.m_actor = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600271F RID: 10015 RVA: 0x0008E84A File Offset: 0x0008CA4A
		// (set) Token: 0x06002720 RID: 10016 RVA: 0x0008E852 File Offset: 0x0008CA52
		public object BootstrapContext
		{
			get
			{
				return this.m_bootstrapContext;
			}
			[SecurityCritical]
			set
			{
				this.m_bootstrapContext = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06002721 RID: 10017 RVA: 0x0008E85C File Offset: 0x0008CA5C
		public virtual IEnumerable<Claim> Claims
		{
			get
			{
				int num;
				for (int i = 0; i < this.m_instanceClaims.Count; i = num + 1)
				{
					yield return this.m_instanceClaims[i];
					num = i;
				}
				if (this.m_externalClaims != null)
				{
					for (int j = 0; j < this.m_externalClaims.Count; j = num + 1)
					{
						if (this.m_externalClaims[j] != null)
						{
							foreach (Claim claim in this.m_externalClaims[j])
							{
								yield return claim;
							}
							IEnumerator<Claim> enumerator = null;
						}
						num = j;
					}
				}
				yield break;
				yield break;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06002722 RID: 10018 RVA: 0x0008E879 File Offset: 0x0008CA79
		protected virtual byte[] CustomSerializationData
		{
			get
			{
				return this.m_userSerializationData;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06002723 RID: 10019 RVA: 0x0008E881 File Offset: 0x0008CA81
		internal Collection<IEnumerable<Claim>> ExternalClaims
		{
			[FriendAccessAllowed]
			get
			{
				return this.m_externalClaims;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x0008E889 File Offset: 0x0008CA89
		// (set) Token: 0x06002725 RID: 10021 RVA: 0x0008E891 File Offset: 0x0008CA91
		public string Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06002726 RID: 10022 RVA: 0x0008E89C File Offset: 0x0008CA9C
		public virtual string Name
		{
			get
			{
				Claim claim = this.FindFirst(this.m_nameType);
				if (claim != null)
				{
					return claim.Value;
				}
				return null;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06002727 RID: 10023 RVA: 0x0008E8C1 File Offset: 0x0008CAC1
		public string NameClaimType
		{
			get
			{
				return this.m_nameType;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06002728 RID: 10024 RVA: 0x0008E8C9 File Offset: 0x0008CAC9
		public string RoleClaimType
		{
			get
			{
				return this.m_roleType;
			}
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x0008E8D4 File Offset: 0x0008CAD4
		public virtual ClaimsIdentity Clone()
		{
			ClaimsIdentity claimsIdentity = new ClaimsIdentity(this.m_instanceClaims);
			claimsIdentity.m_authenticationType = this.m_authenticationType;
			claimsIdentity.m_bootstrapContext = this.m_bootstrapContext;
			claimsIdentity.m_label = this.m_label;
			claimsIdentity.m_nameType = this.m_nameType;
			claimsIdentity.m_roleType = this.m_roleType;
			if (this.Actor != null)
			{
				if (this.IsCircular(this.Actor))
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperationException_ActorGraphCircular"));
				}
				if (!AppContextSwitches.SetActorAsReferenceWhenCopyingClaimsIdentity)
				{
					claimsIdentity.Actor = this.Actor.Clone();
				}
				else
				{
					claimsIdentity.Actor = this.Actor;
				}
			}
			return claimsIdentity;
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x0008E978 File Offset: 0x0008CB78
		[SecurityCritical]
		public virtual void AddClaim(Claim claim)
		{
			if (claim == null)
			{
				throw new ArgumentNullException("claim");
			}
			if (claim.Subject == this)
			{
				this.m_instanceClaims.Add(claim);
				return;
			}
			this.m_instanceClaims.Add(claim.Clone(this));
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x0008E9B0 File Offset: 0x0008CBB0
		[SecurityCritical]
		public virtual void AddClaims(IEnumerable<Claim> claims)
		{
			if (claims == null)
			{
				throw new ArgumentNullException("claims");
			}
			foreach (Claim claim in claims)
			{
				if (claim != null)
				{
					this.AddClaim(claim);
				}
			}
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x0008EA0C File Offset: 0x0008CC0C
		[SecurityCritical]
		public virtual bool TryRemoveClaim(Claim claim)
		{
			bool result = false;
			for (int i = 0; i < this.m_instanceClaims.Count; i++)
			{
				if (this.m_instanceClaims[i] == claim)
				{
					this.m_instanceClaims.RemoveAt(i);
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x0008EA51 File Offset: 0x0008CC51
		[SecurityCritical]
		public virtual void RemoveClaim(Claim claim)
		{
			if (!this.TryRemoveClaim(claim))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ClaimCannotBeRemoved", new object[]
				{
					claim
				}));
			}
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x0008EA78 File Offset: 0x0008CC78
		[SecuritySafeCritical]
		private void SafeAddClaims(IEnumerable<Claim> claims)
		{
			foreach (Claim claim in claims)
			{
				if (claim.Subject == this)
				{
					this.m_instanceClaims.Add(claim);
				}
				else
				{
					this.m_instanceClaims.Add(claim.Clone(this));
				}
			}
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x0008EAE4 File Offset: 0x0008CCE4
		[SecuritySafeCritical]
		private void SafeAddClaim(Claim claim)
		{
			if (claim.Subject == this)
			{
				this.m_instanceClaims.Add(claim);
				return;
			}
			this.m_instanceClaims.Add(claim.Clone(this));
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x0008EB10 File Offset: 0x0008CD10
		public virtual IEnumerable<Claim> FindAll(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			List<Claim> list = new List<Claim>();
			foreach (Claim claim in this.Claims)
			{
				if (match(claim))
				{
					list.Add(claim);
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x0008EB80 File Offset: 0x0008CD80
		public virtual IEnumerable<Claim> FindAll(string type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			List<Claim> list = new List<Claim>();
			foreach (Claim claim in this.Claims)
			{
				if (claim != null && string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase))
				{
					list.Add(claim);
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x0008EBFC File Offset: 0x0008CDFC
		public virtual bool HasClaim(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			foreach (Claim obj in this.Claims)
			{
				if (match(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x0008EC60 File Offset: 0x0008CE60
		public virtual bool HasClaim(string type, string value)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (Claim claim in this.Claims)
			{
				if (claim != null && claim != null && string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase) && string.Equals(claim.Value, value, StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x0008ECF0 File Offset: 0x0008CEF0
		public virtual Claim FindFirst(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			foreach (Claim claim in this.Claims)
			{
				if (match(claim))
				{
					return claim;
				}
			}
			return null;
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x0008ED54 File Offset: 0x0008CF54
		public virtual Claim FindFirst(string type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			foreach (Claim claim in this.Claims)
			{
				if (claim != null && string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase))
				{
					return claim;
				}
			}
			return null;
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x0008EDC4 File Offset: 0x0008CFC4
		[OnSerializing]
		[SecurityCritical]
		private void OnSerializingMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.m_serializedClaims = this.SerializeClaims();
			this.m_serializedNameType = this.m_nameType;
			this.m_serializedRoleType = this.m_roleType;
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x0008EDF4 File Offset: 0x0008CFF4
		[OnDeserialized]
		[SecurityCritical]
		private void OnDeserializedMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.m_serializedClaims))
			{
				this.DeserializeClaims(this.m_serializedClaims);
				this.m_serializedClaims = null;
			}
			this.m_nameType = (string.IsNullOrEmpty(this.m_serializedNameType) ? "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name" : this.m_serializedNameType);
			this.m_roleType = (string.IsNullOrEmpty(this.m_serializedRoleType) ? "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" : this.m_serializedRoleType);
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x0008EE6A File Offset: 0x0008D06A
		[OnDeserializing]
		private void OnDeserializingMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x0008EE8C File Offset: 0x0008D08C
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			info.AddValue("System.Security.ClaimsIdentity.version", this.m_version);
			if (!string.IsNullOrEmpty(this.m_authenticationType))
			{
				info.AddValue("System.Security.ClaimsIdentity.authenticationType", this.m_authenticationType);
			}
			info.AddValue("System.Security.ClaimsIdentity.nameClaimType", this.m_nameType);
			info.AddValue("System.Security.ClaimsIdentity.roleClaimType", this.m_roleType);
			if (!string.IsNullOrEmpty(this.m_label))
			{
				info.AddValue("System.Security.ClaimsIdentity.label", this.m_label);
			}
			if (this.m_actor != null)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					binaryFormatter.Serialize(memoryStream, this.m_actor, null, false);
					info.AddValue("System.Security.ClaimsIdentity.actor", Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length));
				}
			}
			info.AddValue("System.Security.ClaimsIdentity.claims", this.SerializeClaims());
			if (this.m_bootstrapContext != null)
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					binaryFormatter.Serialize(memoryStream2, this.m_bootstrapContext, null, false);
					info.AddValue("System.Security.ClaimsIdentity.bootstrapContext", Convert.ToBase64String(memoryStream2.GetBuffer(), 0, (int)memoryStream2.Length));
				}
			}
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x0008EFD8 File Offset: 0x0008D1D8
		[SecurityCritical]
		private void DeserializeClaims(string serializedClaims)
		{
			if (!string.IsNullOrEmpty(serializedClaims))
			{
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(serializedClaims)))
				{
					this.m_instanceClaims = (List<Claim>)new BinaryFormatter().Deserialize(memoryStream, null, false);
					for (int i = 0; i < this.m_instanceClaims.Count; i++)
					{
						this.m_instanceClaims[i].Subject = this;
					}
				}
			}
			if (this.m_instanceClaims == null)
			{
				this.m_instanceClaims = new List<Claim>();
			}
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x0008F068 File Offset: 0x0008D268
		[SecurityCritical]
		private string SerializeClaims()
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new BinaryFormatter().Serialize(memoryStream, this.m_instanceClaims, null, false);
				result = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			}
			return result;
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x0008F0C0 File Offset: 0x0008D2C0
		private bool IsCircular(ClaimsIdentity subject)
		{
			if (this == subject)
			{
				return true;
			}
			ClaimsIdentity claimsIdentity = subject;
			while (claimsIdentity.Actor != null)
			{
				if (this == claimsIdentity.Actor)
				{
					return true;
				}
				claimsIdentity = claimsIdentity.Actor;
			}
			return false;
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x0008F0F4 File Offset: 0x0008D2F4
		private void Initialize(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			ClaimsIdentity.SerializationMask serializationMask = (ClaimsIdentity.SerializationMask)reader.ReadInt32();
			if ((serializationMask & ClaimsIdentity.SerializationMask.AuthenticationType) == ClaimsIdentity.SerializationMask.AuthenticationType)
			{
				this.m_authenticationType = reader.ReadString();
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.BootstrapConext) == ClaimsIdentity.SerializationMask.BootstrapConext)
			{
				this.m_bootstrapContext = reader.ReadString();
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.NameClaimType) == ClaimsIdentity.SerializationMask.NameClaimType)
			{
				this.m_nameType = reader.ReadString();
			}
			else
			{
				this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.RoleClaimType) == ClaimsIdentity.SerializationMask.RoleClaimType)
			{
				this.m_roleType = reader.ReadString();
			}
			else
			{
				this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.HasClaims) == ClaimsIdentity.SerializationMask.HasClaims)
			{
				int num = reader.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					Claim item = new Claim(reader, this);
					this.m_instanceClaims.Add(item);
				}
			}
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x0008F1A7 File Offset: 0x0008D3A7
		protected virtual Claim CreateClaim(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return new Claim(reader, this);
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x0008F1BE File Offset: 0x0008D3BE
		public virtual void WriteTo(BinaryWriter writer)
		{
			this.WriteTo(writer, null);
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x0008F1C8 File Offset: 0x0008D3C8
		protected virtual void WriteTo(BinaryWriter writer, byte[] userData)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int num = 0;
			ClaimsIdentity.SerializationMask serializationMask = ClaimsIdentity.SerializationMask.None;
			if (this.m_authenticationType != null)
			{
				serializationMask |= ClaimsIdentity.SerializationMask.AuthenticationType;
				num++;
			}
			if (this.m_bootstrapContext != null)
			{
				string text = this.m_bootstrapContext as string;
				if (text != null)
				{
					serializationMask |= ClaimsIdentity.SerializationMask.BootstrapConext;
					num++;
				}
			}
			if (!string.Equals(this.m_nameType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", StringComparison.Ordinal))
			{
				serializationMask |= ClaimsIdentity.SerializationMask.NameClaimType;
				num++;
			}
			if (!string.Equals(this.m_roleType, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", StringComparison.Ordinal))
			{
				serializationMask |= ClaimsIdentity.SerializationMask.RoleClaimType;
				num++;
			}
			if (!string.IsNullOrWhiteSpace(this.m_label))
			{
				serializationMask |= ClaimsIdentity.SerializationMask.HasLabel;
				num++;
			}
			if (this.m_instanceClaims.Count > 0)
			{
				serializationMask |= ClaimsIdentity.SerializationMask.HasClaims;
				num++;
			}
			if (this.m_actor != null)
			{
				serializationMask |= ClaimsIdentity.SerializationMask.Actor;
				num++;
			}
			if (userData != null && userData.Length != 0)
			{
				num++;
				serializationMask |= ClaimsIdentity.SerializationMask.UserData;
			}
			writer.Write((int)serializationMask);
			writer.Write(num);
			if ((serializationMask & ClaimsIdentity.SerializationMask.AuthenticationType) == ClaimsIdentity.SerializationMask.AuthenticationType)
			{
				writer.Write(this.m_authenticationType);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.BootstrapConext) == ClaimsIdentity.SerializationMask.BootstrapConext)
			{
				writer.Write(this.m_bootstrapContext as string);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.NameClaimType) == ClaimsIdentity.SerializationMask.NameClaimType)
			{
				writer.Write(this.m_nameType);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.RoleClaimType) == ClaimsIdentity.SerializationMask.RoleClaimType)
			{
				writer.Write(this.m_roleType);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.HasLabel) == ClaimsIdentity.SerializationMask.HasLabel)
			{
				writer.Write(this.m_label);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.HasClaims) == ClaimsIdentity.SerializationMask.HasClaims)
			{
				writer.Write(this.m_instanceClaims.Count);
				foreach (Claim claim in this.m_instanceClaims)
				{
					claim.WriteTo(writer);
				}
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.Actor) == ClaimsIdentity.SerializationMask.Actor)
			{
				this.m_actor.WriteTo(writer);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.UserData) == ClaimsIdentity.SerializationMask.UserData)
			{
				writer.Write(userData.Length);
				writer.Write(userData);
			}
			writer.Flush();
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x0008F3A8 File Offset: 0x0008D5A8
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		private void Deserialize(SerializationInfo info, StreamingContext context, bool useContext)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			BinaryFormatter binaryFormatter;
			if (useContext)
			{
				binaryFormatter = new BinaryFormatter(null, context);
			}
			else
			{
				binaryFormatter = new BinaryFormatter();
			}
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 959168042U)
				{
					if (num <= 623923795U)
					{
						if (num != 373632733U)
						{
							if (num == 623923795U)
							{
								if (name == "System.Security.ClaimsIdentity.roleClaimType")
								{
									this.m_roleType = info.GetString("System.Security.ClaimsIdentity.roleClaimType");
								}
							}
						}
						else if (name == "System.Security.ClaimsIdentity.label")
						{
							this.m_label = info.GetString("System.Security.ClaimsIdentity.label");
						}
					}
					else if (num != 656336169U)
					{
						if (num == 959168042U)
						{
							if (name == "System.Security.ClaimsIdentity.nameClaimType")
							{
								this.m_nameType = info.GetString("System.Security.ClaimsIdentity.nameClaimType");
							}
						}
					}
					else if (name == "System.Security.ClaimsIdentity.authenticationType")
					{
						this.m_authenticationType = info.GetString("System.Security.ClaimsIdentity.authenticationType");
					}
				}
				else if (num <= 1476368026U)
				{
					if (num != 1453716852U)
					{
						if (num != 1476368026U)
						{
							continue;
						}
						if (!(name == "System.Security.ClaimsIdentity.actor"))
						{
							continue;
						}
						using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(info.GetString("System.Security.ClaimsIdentity.actor"))))
						{
							this.m_actor = (ClaimsIdentity)binaryFormatter.Deserialize(memoryStream, null, false);
							continue;
						}
					}
					else if (!(name == "System.Security.ClaimsIdentity.claims"))
					{
						continue;
					}
					this.DeserializeClaims(info.GetString("System.Security.ClaimsIdentity.claims"));
				}
				else if (num != 2480284791U)
				{
					if (num == 3659022112U)
					{
						if (name == "System.Security.ClaimsIdentity.bootstrapContext")
						{
							using (MemoryStream memoryStream2 = new MemoryStream(Convert.FromBase64String(info.GetString("System.Security.ClaimsIdentity.bootstrapContext"))))
							{
								this.m_bootstrapContext = binaryFormatter.Deserialize(memoryStream2, null, false);
							}
						}
					}
				}
				else if (name == "System.Security.ClaimsIdentity.version")
				{
					string @string = info.GetString("System.Security.ClaimsIdentity.version");
				}
			}
		}

		// Token: 0x04000ED1 RID: 3793
		[NonSerialized]
		private byte[] m_userSerializationData;

		// Token: 0x04000ED2 RID: 3794
		[NonSerialized]
		private const string PreFix = "System.Security.ClaimsIdentity.";

		// Token: 0x04000ED3 RID: 3795
		[NonSerialized]
		private const string ActorKey = "System.Security.ClaimsIdentity.actor";

		// Token: 0x04000ED4 RID: 3796
		[NonSerialized]
		private const string AuthenticationTypeKey = "System.Security.ClaimsIdentity.authenticationType";

		// Token: 0x04000ED5 RID: 3797
		[NonSerialized]
		private const string BootstrapContextKey = "System.Security.ClaimsIdentity.bootstrapContext";

		// Token: 0x04000ED6 RID: 3798
		[NonSerialized]
		private const string ClaimsKey = "System.Security.ClaimsIdentity.claims";

		// Token: 0x04000ED7 RID: 3799
		[NonSerialized]
		private const string LabelKey = "System.Security.ClaimsIdentity.label";

		// Token: 0x04000ED8 RID: 3800
		[NonSerialized]
		private const string NameClaimTypeKey = "System.Security.ClaimsIdentity.nameClaimType";

		// Token: 0x04000ED9 RID: 3801
		[NonSerialized]
		private const string RoleClaimTypeKey = "System.Security.ClaimsIdentity.roleClaimType";

		// Token: 0x04000EDA RID: 3802
		[NonSerialized]
		private const string VersionKey = "System.Security.ClaimsIdentity.version";

		// Token: 0x04000EDB RID: 3803
		[NonSerialized]
		public const string DefaultIssuer = "LOCAL AUTHORITY";

		// Token: 0x04000EDC RID: 3804
		[NonSerialized]
		public const string DefaultNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

		// Token: 0x04000EDD RID: 3805
		[NonSerialized]
		public const string DefaultRoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

		// Token: 0x04000EDE RID: 3806
		[NonSerialized]
		private List<Claim> m_instanceClaims;

		// Token: 0x04000EDF RID: 3807
		[NonSerialized]
		private Collection<IEnumerable<Claim>> m_externalClaims;

		// Token: 0x04000EE0 RID: 3808
		[NonSerialized]
		private string m_nameType;

		// Token: 0x04000EE1 RID: 3809
		[NonSerialized]
		private string m_roleType;

		// Token: 0x04000EE2 RID: 3810
		[OptionalField(VersionAdded = 2)]
		private string m_version;

		// Token: 0x04000EE3 RID: 3811
		[OptionalField(VersionAdded = 2)]
		private ClaimsIdentity m_actor;

		// Token: 0x04000EE4 RID: 3812
		[OptionalField(VersionAdded = 2)]
		private string m_authenticationType;

		// Token: 0x04000EE5 RID: 3813
		[OptionalField(VersionAdded = 2)]
		private object m_bootstrapContext;

		// Token: 0x04000EE6 RID: 3814
		[OptionalField(VersionAdded = 2)]
		private string m_label;

		// Token: 0x04000EE7 RID: 3815
		[OptionalField(VersionAdded = 2)]
		private string m_serializedNameType;

		// Token: 0x04000EE8 RID: 3816
		[OptionalField(VersionAdded = 2)]
		private string m_serializedRoleType;

		// Token: 0x04000EE9 RID: 3817
		[OptionalField(VersionAdded = 2)]
		private string m_serializedClaims;

		// Token: 0x02000B1A RID: 2842
		private enum SerializationMask
		{
			// Token: 0x040032FD RID: 13053
			None,
			// Token: 0x040032FE RID: 13054
			AuthenticationType,
			// Token: 0x040032FF RID: 13055
			BootstrapConext,
			// Token: 0x04003300 RID: 13056
			NameClaimType = 4,
			// Token: 0x04003301 RID: 13057
			RoleClaimType = 8,
			// Token: 0x04003302 RID: 13058
			HasClaims = 16,
			// Token: 0x04003303 RID: 13059
			HasLabel = 32,
			// Token: 0x04003304 RID: 13060
			Actor = 64,
			// Token: 0x04003305 RID: 13061
			UserData = 128
		}
	}
}
