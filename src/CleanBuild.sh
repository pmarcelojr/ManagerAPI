#!/usr/bin/env bash
## Clean And Build all Projects

## Saia ao Primeiro Erro
set -e

## Cores
export vermelho="\e[1;31m"
export verde="\e[1;32m"
export corlogo="\033[1;34m"

check_successful() {
    if [ $? != 0 ];
    then
        echo -e $vermelho "Error Execution"
        exit 1
    fi
}

## Titulo
echo -e "${corlogo} Script de Automação para Clean e Build de todos os projetos!"
sleep 2

cd Manager.API
dotnet clean
dotnet build
check_successful
echo '***** Manager.API Concluído *****'
sleep 1
cd ..
cd Manager.Core
dotnet clean
dotnet build
check_successful
echo '***** Manager.Core Concluído *****'
sleep 1
cd ..
cd Manager.Domain
dotnet clean
dotnet build
check_successful
echo '***** Manager.Domain Concluído *****'
sleep 1
cd ..
cd Manager.Infra
dotnet clean
dotnet build
check_successful
echo '***** Manager.Infra Concluído *****'
sleep 1
cd ..
cd Manager.Services
dotnet clean
dotnet build
check_successful
echo '***** Manager.Services Concluído *****'
sleep 1
cd ..

echo -e "${verde} Script de Automação Concluído com Sucesso!"