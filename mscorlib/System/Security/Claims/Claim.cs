using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;

namespace System.Security.Claims
{
	// Token: 0x020002EF RID: 751
	[Serializable]
	public class Claim
	{
		// Token: 0x060026F3 RID: 9971 RVA: 0x0008DCFB File Offset: 0x0008BEFB
		public Claim(BinaryReader reader) : this(reader, null)
		{
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x0008DD05 File Offset: 0x0008BF05
		public Claim(BinaryReader reader, ClaimsIdentity subject)
		{
			this.m_propertyLock = new object();
			base..ctor();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Initialize(reader, subject);
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x0008DD2E File Offset: 0x0008BF2E
		public Claim(string type, string value) : this(type, value, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", null)
		{
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x0008DD48 File Offset: 0x0008BF48
		public Claim(string type, string value, string valueType) : this(type, value, valueType, "LOCAL AUTHORITY", "LOCAL AUTHORITY", null)
		{
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x0008DD5E File Offset: 0x0008BF5E
		public Claim(string type, string value, string valueType, string issuer) : this(type, value, valueType, issuer, issuer, null)
		{
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x0008DD6E File Offset: 0x0008BF6E
		public Claim(string type, string value, string valueType, string issuer, string originalIssuer) : this(type, value, valueType, issuer, originalIssuer, null)
		{
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x0008DD80 File Offset: 0x0008BF80
		public Claim(string type, string value, string valueType, string issuer, string originalIssuer, ClaimsIdentity subject) : this(type, value, valueType, issuer, originalIssuer, subject, null, null)
		{
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x0008DDA0 File Offset: 0x0008BFA0
		internal Claim(string type, string value, string valueType, string issuer, string originalIssuer, ClaimsIdentity subject, string propertyKey, string propertyValue)
		{
			this.m_propertyLock = new object();
			base..ctor();
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_type = type;
			this.m_value = value;
			if (string.IsNullOrEmpty(valueType))
			{
				this.m_valueType = "http://www.w3.org/2001/XMLSchema#string";
			}
			else
			{
				this.m_valueType = valueType;
			}
			if (string.IsNullOrEmpty(issuer))
			{
				this.m_issuer = "LOCAL AUTHORITY";
			}
			else
			{
				this.m_issuer = issuer;
			}
			if (string.IsNullOrEmpty(originalIssuer))
			{
				this.m_originalIssuer = this.m_issuer;
			}
			else
			{
				this.m_originalIssuer = originalIssuer;
			}
			this.m_subject = subject;
			if (propertyKey != null)
			{
				this.Properties.Add(propertyKey, propertyValue);
			}
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x0008DE5C File Offset: 0x0008C05C
		protected Claim(Claim other) : this(other, (other == null) ? null : other.m_subject)
		{
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x0008DE74 File Offset: 0x0008C074
		protected Claim(Claim other, ClaimsIdentity subject)
		{
			this.m_propertyLock = new object();
			base..ctor();
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.m_issuer = other.m_issuer;
			this.m_originalIssuer = other.m_originalIssuer;
			this.m_subject = subject;
			this.m_type = other.m_type;
			this.m_value = other.m_value;
			this.m_valueType = other.m_valueType;
			if (other.m_properties != null)
			{
				this.m_properties = new Dictionary<string, string>();
				foreach (string key in other.m_properties.Keys)
				{
					this.m_properties.Add(key, other.m_properties[key]);
				}
			}
			if (other.m_userSerializationData != null)
			{
				this.m_userSerializationData = (other.m_userSerializationData.Clone() as byte[]);
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060026FD RID: 9981 RVA: 0x0008DF70 File Offset: 0x0008C170
		protected virtual byte[] CustomSerializationData
		{
			get
			{
				return this.m_userSerializationData;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060026FE RID: 9982 RVA: 0x0008DF78 File Offset: 0x0008C178
		public string Issuer
		{
			get
			{
				return this.m_issuer;
			}
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x0008DF80 File Offset: 0x0008C180
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			this.m_propertyLock = new object();
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06002700 RID: 9984 RVA: 0x0008DF8D File Offset: 0x0008C18D
		public string OriginalIssuer
		{
			get
			{
				return this.m_originalIssuer;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06002701 RID: 9985 RVA: 0x0008DF98 File Offset: 0x0008C198
		public IDictionary<string, string> Properties
		{
			get
			{
				if (this.m_properties == null)
				{
					object propertyLock = this.m_propertyLock;
					lock (propertyLock)
					{
						if (this.m_properties == null)
						{
							this.m_properties = new Dictionary<string, string>();
						}
					}
				}
				return this.m_properties;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x0008DFF4 File Offset: 0x0008C1F4
		// (set) Token: 0x06002703 RID: 9987 RVA: 0x0008DFFC File Offset: 0x0008C1FC
		public ClaimsIdentity Subject
		{
			get
			{
				return this.m_subject;
			}
			internal set
			{
				this.m_subject = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x0008E005 File Offset: 0x0008C205
		public string Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06002705 RID: 9989 RVA: 0x0008E00D File Offset: 0x0008C20D
		public string Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06002706 RID: 9990 RVA: 0x0008E015 File Offset: 0x0008C215
		public string ValueType
		{
			get
			{
				return this.m_valueType;
			}
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x0008E01D File Offset: 0x0008C21D
		public virtual Claim Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x0008E026 File Offset: 0x0008C226
		public virtual Claim Clone(ClaimsIdentity identity)
		{
			return new Claim(this, identity);
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x0008E030 File Offset: 0x0008C230
		private void Initialize(BinaryReader reader, ClaimsIdentity subject)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.m_subject = subject;
			Claim.SerializationMask serializationMask = (Claim.SerializationMask)reader.ReadInt32();
			int num = 1;
			int num2 = reader.ReadInt32();
			this.m_value = reader.ReadString();
			if ((serializationMask & Claim.SerializationMask.NameClaimType) == Claim.SerializationMask.NameClaimType)
			{
				this.m_type = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			}
			else if ((serializationMask & Claim.SerializationMask.RoleClaimType) == Claim.SerializationMask.RoleClaimType)
			{
				this.m_type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			}
			else
			{
				this.m_type = reader.ReadString();
				num++;
			}
			if ((serializationMask & Claim.SerializationMask.StringType) == Claim.SerializationMask.StringType)
			{
				this.m_valueType = reader.ReadString();
				num++;
			}
			else
			{
				this.m_valueType = "http://www.w3.org/2001/XMLSchema#string";
			}
			if ((serializationMask & Claim.SerializationMask.Issuer) == Claim.SerializationMask.Issuer)
			{
				this.m_issuer = reader.ReadString();
				num++;
			}
			else
			{
				this.m_issuer = "LOCAL AUTHORITY";
			}
			if ((serializationMask & Claim.SerializationMask.OriginalIssuerEqualsIssuer) == Claim.SerializationMask.OriginalIssuerEqualsIssuer)
			{
				this.m_originalIssuer = this.m_issuer;
			}
			else if ((serializationMask & Claim.SerializationMask.OriginalIssuer) == Claim.SerializationMask.OriginalIssuer)
			{
				this.m_originalIssuer = reader.ReadString();
				num++;
			}
			else
			{
				this.m_originalIssuer = "LOCAL AUTHORITY";
			}
			if ((serializationMask & Claim.SerializationMask.HasProperties) == Claim.SerializationMask.HasProperties)
			{
				int num3 = reader.ReadInt32();
				for (int i = 0; i < num3; i++)
				{
					this.Properties.Add(reader.ReadString(), reader.ReadString());
				}
			}
			if ((serializationMask & Claim.SerializationMask.UserData) == Claim.SerializationMask.UserData)
			{
				int count = reader.ReadInt32();
				this.m_userSerializationData = reader.ReadBytes(count);
				num++;
			}
			for (int j = num; j < num2; j++)
			{
				reader.ReadString();
			}
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x0008E19A File Offset: 0x0008C39A
		public virtual void WriteTo(BinaryWriter writer)
		{
			this.WriteTo(writer, null);
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x0008E1A4 File Offset: 0x0008C3A4
		protected virtual void WriteTo(BinaryWriter writer, byte[] userData)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int num = 1;
			Claim.SerializationMask serializationMask = Claim.SerializationMask.None;
			if (string.Equals(this.m_type, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"))
			{
				serializationMask |= Claim.SerializationMask.NameClaimType;
			}
			else if (string.Equals(this.m_type, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"))
			{
				serializationMask |= Claim.SerializationMask.RoleClaimType;
			}
			else
			{
				num++;
			}
			if (!string.Equals(this.m_valueType, "http://www.w3.org/2001/XMLSchema#string", StringComparison.Ordinal))
			{
				num++;
				serializationMask |= Claim.SerializationMask.StringType;
			}
			if (!string.Equals(this.m_issuer, "LOCAL AUTHORITY", StringComparison.Ordinal))
			{
				num++;
				serializationMask |= Claim.SerializationMask.Issuer;
			}
			if (string.Equals(this.m_originalIssuer, this.m_issuer, StringComparison.Ordinal))
			{
				serializationMask |= Claim.SerializationMask.OriginalIssuerEqualsIssuer;
			}
			else if (!string.Equals(this.m_originalIssuer, "LOCAL AUTHORITY", StringComparison.Ordinal))
			{
				num++;
				serializationMask |= Claim.SerializationMask.OriginalIssuer;
			}
			if (this.Properties.Count > 0)
			{
				num++;
				serializationMask |= Claim.SerializationMask.HasProperties;
			}
			if (userData != null && userData.Length != 0)
			{
				num++;
				serializationMask |= Claim.SerializationMask.UserData;
			}
			writer.Write((int)serializationMask);
			writer.Write(num);
			writer.Write(this.m_value);
			if ((serializationMask & Claim.SerializationMask.NameClaimType) != Claim.SerializationMask.NameClaimType && (serializationMask & Claim.SerializationMask.RoleClaimType) != Claim.SerializationMask.RoleClaimType)
			{
				writer.Write(this.m_type);
			}
			if ((serializationMask & Claim.SerializationMask.StringType) == Claim.SerializationMask.StringType)
			{
				writer.Write(this.m_valueType);
			}
			if ((serializationMask & Claim.SerializationMask.Issuer) == Claim.SerializationMask.Issuer)
			{
				writer.Write(this.m_issuer);
			}
			if ((serializationMask & Claim.SerializationMask.OriginalIssuer) == Claim.SerializationMask.OriginalIssuer)
			{
				writer.Write(this.m_originalIssuer);
			}
			if ((serializationMask & Claim.SerializationMask.HasProperties) == Claim.SerializationMask.HasProperties)
			{
				writer.Write(this.Properties.Count);
				foreach (string text in this.Properties.Keys)
				{
					writer.Write(text);
					writer.Write(this.Properties[text]);
				}
			}
			if ((serializationMask & Claim.SerializationMask.UserData) == Claim.SerializationMask.UserData)
			{
				writer.Write(userData.Length);
				writer.Write(userData);
			}
			writer.Flush();
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x0008E38C File Offset: 0x0008C58C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}: {1}", this.m_type, this.m_value);
		}

		// Token: 0x04000EC8 RID: 3784
		private string m_issuer;

		// Token: 0x04000EC9 RID: 3785
		private string m_originalIssuer;

		// Token: 0x04000ECA RID: 3786
		private string m_type;

		// Token: 0x04000ECB RID: 3787
		private string m_value;

		// Token: 0x04000ECC RID: 3788
		private string m_valueType;

		// Token: 0x04000ECD RID: 3789
		[NonSerialized]
		private byte[] m_userSerializationData;

		// Token: 0x04000ECE RID: 3790
		private Dictionary<string, string> m_properties;

		// Token: 0x04000ECF RID: 3791
		[NonSerialized]
		private object m_propertyLock;

		// Token: 0x04000ED0 RID: 3792
		[NonSerialized]
		private ClaimsIdentity m_subject;

		// Token: 0x02000B19 RID: 2841
		private enum SerializationMask
		{
			// Token: 0x040032F3 RID: 13043
			None,
			// Token: 0x040032F4 RID: 13044
			NameClaimType,
			// Token: 0x040032F5 RID: 13045
			RoleClaimType,
			// Token: 0x040032F6 RID: 13046
			StringType = 4,
			// Token: 0x040032F7 RID: 13047
			Issuer = 8,
			// Token: 0x040032F8 RID: 13048
			OriginalIssuerEqualsIssuer = 16,
			// Token: 0x040032F9 RID: 13049
			OriginalIssuer = 32,
			// Token: 0x040032FA RID: 13050
			HasProperties = 64,
			// Token: 0x040032FB RID: 13051
			UserData = 128
		}
	}
}
