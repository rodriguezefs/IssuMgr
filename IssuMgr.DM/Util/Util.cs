using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace IssuMgr.Util {
    public static class Ext {

        /// <summary>
        /// Convierte un Datatable en un List&lt;T&gt;
        /// </summary>
        /// <typeparam name="T">Objeto (clase) genérica</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt) {
            List<T> data = new List<T>();
            foreach(DataRow row in dt.Rows) {
                T item = row.ToRow<T>();
                data.Add(item);
            }
            return data;
        }

        /// <summary>
        /// Retorna un Objeto (clase) genérica basado en una DataRow
        /// </summary>
        /// <typeparam name="T">Objeto</typeparam>
        /// <param name="dr">DataRow</param>
        /// <returns></returns>
        public static T ToRow<T>(this DataRow dr) {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach(DataColumn column in dr.Table.Columns) {
                foreach(PropertyInfo pro in temp.GetProperties()) {
                    if(pro.Name == column.ColumnName) {
                        if(column.DataType == Type.GetType("System.String")) {
                            pro.SetValue(obj, dr[column.ColumnName].ToString().Trim(), null);
                        } else {
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                    } else {
                        continue;
                    }
                }
            }
            return obj;
        }

        /// <summary>
        /// Retorna el nombre del miembro actual (método) de una clase
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public static string GetCaller([CallerMemberName] string memberName = "") {
            return memberName;
        }
    }
}
