import * as React from "react";
import { CryptoType } from "../CryptoType";
import {} from "@material-ui/data-grid";
import {
  makeStyles,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from "@material-ui/core";
interface Props {
  cryptos?: CryptoType[];
}

const CryptoList: React.FC<Props> = ({ cryptos }) => {
  cryptos?.sort((a, b) =>
    a.marketCap < b.marketCap ? 1 : a.marketCap > b.marketCap ? -1 : 0
  );

  function numberWithCommas(num: number): string {
    var num_parts = num.toString().split(".");
    num_parts[0] = num_parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");

    return num_parts.join(".");
  }
  return (
    <TableContainer component={Paper}>
      <Table aria-label='crypto table'>
        <TableHead>
          <TableRow>
            <TableCell>Coin</TableCell>
            <TableCell align='right'>Price</TableCell>
            <TableCell align='right'>24h change difference</TableCell>
            <TableCell align='right'>7d change difference</TableCell>
            <TableCell align='right'>1 month change difference</TableCell>
            <TableCell align='right'>Volume</TableCell>
            <TableCell align='right'>Market Cap</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {cryptos?.map((item) => (
            <TableRow key={item.id}>
              <TableCell
                component='th'
                scope='row'
                style={{ fontWeight: "bold" }}
              >
                {item.currencyName}
              </TableCell>
              <TableCell align='right'>
                ${numberWithCommas(item.price)}
              </TableCell>
              <TableCell
                align='right'
                style={
                  item.differenceIn24Hrs < 0
                    ? { color: "red" }
                    : { color: "green" }
                }
              >
                {item.differenceIn24Hrs}%
              </TableCell>
              <TableCell
                align='right'
                style={
                  item.differenceIn24Hrs < 0
                    ? { color: "red" }
                    : { color: "green" }
                }
              >
                {item.differenceIn7Days}%
              </TableCell>
              <TableCell
                align='right'
                style={
                  item.differenceIn24Hrs < 0
                    ? { color: "red" }
                    : { color: "green" }
                }
              >
                {item.differenceInMonth}%
              </TableCell>
              <TableCell align='right'>
                ${numberWithCommas(item.volume)}
              </TableCell>
              <TableCell align='right'>
                ${numberWithCommas(item.marketCap)}
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default CryptoList;
