using System;
using System.IdentityModel.Tokens;
using System.Linq;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x02000104 RID: 260
	internal class V1ProfileAppInfo
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x00038E94 File Offset: 0x00037094
		public V1ProfileAppInfo(OAuthTokenHandler.V1ProfileTokenHandlerBase handler, JwtSecurityToken token)
		{
			JwtPayload payload = token.Payload;
			this.AppId = handler.GetClaimValue(token, Constants.ClaimTypes.AppId);
			this.Scope = OAuthCommon.TryGetClaimValue(token.Payload, Constants.ClaimTypes.Scp);
			object obj;
			if (token.Payload.TryGetValue(Constants.ClaimTypes.Roles, out obj))
			{
				object[] array = obj as object[];
				string text = obj as string;
				if (array != null)
				{
					if (array.All((object x) => x.GetType() == typeof(string)))
					{
						this.Role = string.Join(" ", array);
						return;
					}
				}
				if (text != null && !text.Contains(" "))
				{
					this.Role = text;
					return;
				}
				handler.Throw(OAuthErrors.InvalidClaimValueFound, new object[]
				{
					obj.SerializeToJson(),
					Constants.ClaimTypes.Roles
				}, null, null);
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00038F73 File Offset: 0x00037173
		public V1ProfileAppInfo(string appId, string scp, string rol = null)
		{
			this.AppId = appId;
			this.Scope = scp;
			this.Role = rol;
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x00038F90 File Offset: 0x00037190
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x00038F98 File Offset: 0x00037198
		public string AppId { get; private set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00038FA1 File Offset: 0x000371A1
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x00038FA9 File Offset: 0x000371A9
		public string Scope { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00038FB2 File Offset: 0x000371B2
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x00038FBA File Offset: 0x000371BA
		public string Role { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00038FC4 File Offset: 0x000371C4
		public bool ContainsFullAccessRole
		{
			get
			{
				return !string.IsNullOrEmpty(this.Role) && this.Role.Split(new char[]
				{
					' '
				}, StringSplitOptions.RemoveEmptyEntries).Contains(Constants.ClaimValues.FullAccess);
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00039003 File Offset: 0x00037203
		public override string ToString()
		{
			return this.AppId;
		}
	}
}
