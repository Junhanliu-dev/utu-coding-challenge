import { Container, LinearProgress } from "@material-ui/core";
import React from "react";
import { useQuery } from "react-query";
import "./App.css";
import { CryptoType } from "./CryptoType";
import CryptoList from "./features/CryptoList";

const getCryptos = async (): Promise<CryptoType[]> =>
  await await (await fetch("http://localhost:5000/api/crypto")).json();

const App = () => {
  const { data, isLoading, error } = useQuery<CryptoType[]>(
    "crypto",
    getCryptos
  );

  console.log(data);

  if (isLoading) return <LinearProgress />;

  return (
    <Container className='App'>
      <CryptoList cryptos={data} />
    </Container>
  );
};

export default App;
