using quanlyphongGym.DAO;
using quanlyphongGym.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlyphongGym.BUS
{
    public class ThietBiCTL
    {
        private ThietBiDAO data = new ThietBiDAO();
        private ThietBiDTO info = new ThietBiDTO();

        public ThietBiDTO ThietBi
        {
            get { return info; }
            set { info = value; }
        }

        public int countTBType(string type)
        {
            return data.countTBType(type);
        }

        public void insert()
        {
            data.insert(info);
        }

        public void delete()
        {
            data.delete(info.ID_TB);
        }

        public void update()
        {
            data.update(info);
        }

        public void exit()
        {
            data.exit();
        }

        public ArrayList getDsThietBi()
        {
            return data.getDsThietBi();
        }

        public ArrayList getDsThietBi(string keyword)
        {
            return data.getDsThietBi(keyword);
        }
    }
}
