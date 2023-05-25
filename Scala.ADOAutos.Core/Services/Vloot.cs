using Scala.ADOAutos.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scala.ADOAutos.Core.Services
{
    public class Vloot
    {
        public List<Voertuig> GetVoertuigen()
        {
            List<Voertuig> voertuigen = new List<Voertuig>();
            string sql;
            sql = "select * from autos order by merk, serie";
            DataTable dt = DBServices.ExecuteSelect(sql);
            if(dt == null)
            {
                return null;
            }
            else
            {
                foreach(DataRow dr in dt.Rows)
                {
                    string id = dr["id"].ToString();
                    string merk = dr["merk"].ToString();
                    string serie = dr["serie"].ToString();
                    string kleur = dr["kleur"].ToString();
                    int kw = int.Parse(dr["kw"].ToString());
                    string nummerplaat = dr["nummerplaat"].ToString();
                    DateTime indienst = DateTime.Parse(dr["indienst"].ToString());
                    voertuigen.Add(new Voertuig(id, merk, serie, kleur, nummerplaat, kw, indienst));
                }
                return voertuigen;
            }
        }

        public bool VoertuigToevoegen(Voertuig voertuig)
        {
            string sql;
            sql = $"insert into autos(id, merk, serie, kleur, kw, nummerplaat, indienst) values (";
            sql += $"'{voertuig.Id}',";
            sql += $"'{voertuig.Merk}',";
            sql += $"'{voertuig.Serie}',";
            sql += $"'{voertuig.Kleur}',";
            sql += $"{voertuig.KW},";
            sql += $"'{voertuig.Nummerplaat}',";
            sql += $"'{voertuig.InDienst.ToString("yyyy-MM-dd")}')";
            return DBServices.ExecuteCommand(sql);
        }

        public bool VoertuigWijzigen(Voertuig voertuig)
        {
            string sql;
            sql = $"update autos set ";
            sql += $" merk = '{voertuig.Merk}' , ";
            sql += $" serie = '{voertuig.Serie}' , ";
            sql += $" kleur = '{voertuig.Kleur}' , ";
            sql += $" kw = {voertuig.KW} , ";
            sql += $" nummerplaat = '{voertuig.Nummerplaat}' , ";
            sql += $" indienst = '{voertuig.InDienst.ToString("yyyy-MM-dd")}'";
            sql += $" where id = '{voertuig.Id}'";
            return DBServices.ExecuteCommand(sql);

        }
        public bool VoertuigVerwijderen(Voertuig voertuig)
        {
            string sql;
            sql = $"delete from autos where id = '{voertuig.Id}'";
            return DBServices.ExecuteCommand(sql);
        }
    }
}
