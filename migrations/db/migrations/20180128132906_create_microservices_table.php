<?php

use Phinx\Db\Adapter\MysqlAdapter;
use Phinx\Db\Adapter\PostgresAdapter;
use Phinx\Migration\AbstractMigration;

class CreateMicroservicesTable extends AbstractMigration
{
    public function change()
    {
        $table = $this->table('microservices');
        $table->addColumn('service_id', 'integer', [
                'limit' => PostgresAdapter::INT_SMALL,
                'limit' => MysqlAdapter::INT_SMALL,
                'signed' => false
            ])
            ->addColumn('service_type', 'integer', [
                'limit' => PostgresAdapter::INT_SMALL,
                'limit' => MysqlAdapter::INT_SMALL,
                'signed' => false
            ])
            ->addColumn('host', 'string')
            ->addColumn('port', 'integer', [
                'limit' => PostgresAdapter::INT_SMALL,
                'limit' => MysqlAdapter::INT_SMALL,
                'signed' => false
            ])
            ->addColumn('token', 'string')
            ->addColumn('created_at', 'timestamp', ['default' => 'CURRENT_TIMESTAMP'])
            ->create();
    }
}
