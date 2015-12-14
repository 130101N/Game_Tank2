using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{
    class MapRow
    {
        public List<MapCell> Columns = new List<MapCell>();
    }
    class TileMap
    {
        public List<MapRow> Rows = new List<MapRow>();
        public int MapWidth = 50;
        public int MapHeight = 50;

        public TileMap()
        {
            for (int y = 0; y < MapHeight; y++)
            {
                MapRow thisRow = new MapRow();
                for (int x = 0; x < MapWidth; x++)
                {
                    thisRow.Columns.Add(new MapCell(2));
                }
                Rows.Add(thisRow);
            }

            // Create Sample Map Data

            /* Rows[0].Columns[0].TileID = 1;
             Rows[0].Columns[1].TileID = 1;
             Rows[0].Columns[2].TileID = 1;
             Rows[0].Columns[3].TileID = 1;
             Rows[0].Columns[4].TileID = 0;
             Rows[0].Columns[5].TileID = 1;
             Rows[0].Columns[6].TileID = 2;
             Rows[0].Columns[7].TileID = 3;
             Rows[0].Columns[8].TileID = 0;
             Rows[0].Columns[9].TileID = 1;
             Rows[0].Columns[10].TileID = 2;
             Rows[0].Columns[11].TileID = 3;
             Rows[0].Columns[12].TileID = 0;
             Rows[0].Columns[13].TileID = 1;
             Rows[0].Columns[14].TileID = 2;
             Rows[0].Columns[15].TileID = 3;
             Rows[0].Columns[16].TileID = 0;
             Rows[0].Columns[17].TileID = 1;
             Rows[0].Columns[18].TileID = 2;
             Rows[0].Columns[19].TileID = 3;*/


            Rows[1].Columns[7].TileID = 1;

            Rows[2].Columns[6].TileID = 3;
            Rows[2].Columns[7].TileID = 0;
            Rows[2].Columns[9].TileID = 1;

            Rows[3].Columns[0].TileID = 3;
            Rows[3].Columns[1].TileID = 1;
            Rows[3].Columns[2].TileID = 0;
            Rows[3].Columns[4].TileID = 1;
            Rows[3].Columns[6].TileID = 0;

            Rows[4].Columns[0].TileID = 1;
            Rows[4].Columns[1].TileID = 0;
            Rows[4].Columns[2].TileID = 3;
            Rows[4].Columns[7].TileID = 0;


            Rows[6].Columns[2].TileID = 1;
            Rows[6].Columns[3].TileID = 0;
            Rows[6].Columns[7].TileID = 3;
            Rows[6].Columns[8].TileID = 0;

            Rows[7].Columns[2].TileID = 1;
            Rows[7].Columns[5].TileID = 3;
            Rows[7].Columns[6].TileID = 0;

            Rows[8].Columns[0].TileID = 0;
            Rows[8].Columns[1].TileID = 1;
            Rows[8].Columns[4].TileID = 3;
            Rows[8].Columns[5].TileID = 2;
            Rows[8].Columns[6].TileID = 3;
            Rows[8].Columns[7].TileID = 1;
            Rows[8].Columns[9].TileID = 0;


        }
    }
}
